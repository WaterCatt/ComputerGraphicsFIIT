using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Lab9
{
    public static class GouraudRenderer
    {
        public static void Draw(Graphics graphics, Polyhedron polyhedron, Camera camera, int width, int height)
        {
            if (width <= 0 || height <= 0 || polyhedron == null || camera == null)
                return;

            Rasterizer.ResetBuffers(width, height, out double[] zBuffer, out int[] pixels);

            double[,] mvp = MatrixOps.Multiply(camera.GetProjectionMatrix(), camera.GetViewMatrix());
            int vertexCount = polyhedron.Vertices.Count;
            var screen = new Point3D[vertexCount];
            var invW = new double[vertexCount];
            var valid = new bool[vertexCount];
            float scale = camera.Type == ProjectionType.Perspective
                ? Math.Min(width, height) / 2f
                : Math.Min(width, height) / (2f * 3.5f);

            for (int index = 0; index < vertexCount; index++)
            {
                Point3D vertex = polyhedron.Vertices[index];
                double x = vertex.X * mvp[0, 0] + vertex.Y * mvp[0, 1] + vertex.Z * mvp[0, 2] + vertex.W * mvp[0, 3];
                double y = vertex.X * mvp[1, 0] + vertex.Y * mvp[1, 1] + vertex.Z * mvp[1, 2] + vertex.W * mvp[1, 3];
                double z = vertex.X * mvp[2, 0] + vertex.Y * mvp[2, 1] + vertex.Z * mvp[2, 2] + vertex.W * mvp[2, 3];
                double w = vertex.X * mvp[3, 0] + vertex.Y * mvp[3, 1] + vertex.Z * mvp[3, 2] + vertex.W * mvp[3, 3];

                if (camera.Type == ProjectionType.Perspective)
                {
                    if (w <= 0.05)
                        continue;
                    invW[index] = 1.0 / w;
                    x /= w;
                    y /= w;
                    z /= w;
                }
                else
                {
                    invW[index] = 1.0;
                    z = -z;
                }

                screen[index] = new Point3D(width / 2.0 + x * scale, height / 2.0 - y * scale, z);
                valid[index] = true;
            }

            if (polyhedron.VertexNormals.Count != vertexCount)
                polyhedron.ComputeVertexNormals();

            foreach (Polygon face in polyhedron.Faces)
            {
                if (face.VertexIndices.Count < 3)
                    continue;

                int cornerA = 0;
                int vertexA = face.VertexIndices[cornerA];
                for (int cornerB = 1; cornerB < face.VertexIndices.Count - 1; cornerB++)
                {
                    int cornerC = cornerB + 1;
                    int vertexB = face.VertexIndices[cornerB];
                    int vertexC = face.VertexIndices[cornerC];
                    if (!IsValidVertex(vertexA, valid) || !IsValidVertex(vertexB, valid) || !IsValidVertex(vertexC, valid))
                        continue;

                    Color colorA = ShadeCorner(polyhedron, face, cornerA, vertexA, camera);
                    Color colorB = ShadeCorner(polyhedron, face, cornerB, vertexB, camera);
                    Color colorC = ShadeCorner(polyhedron, face, cornerC, vertexC, camera);
                    double invWA = invW[vertexA], invWB = invW[vertexB], invWC = invW[vertexC];

                    Rasterizer.FillTriangle(screen[vertexA], screen[vertexB], screen[vertexC], width, height,
                        (pixelIndex, baryA, baryB, baryC, depth) =>
                        {
                            if (depth >= zBuffer[pixelIndex])
                                return;

                            double weightA = baryA * invWA;
                            double weightB = baryB * invWB;
                            double weightC = baryC * invWC;
                            double total = weightA + weightB + weightC;
                            if (total <= 1e-12)
                                return;

                            weightA /= total;
                            weightB /= total;
                            weightC /= total;
                            zBuffer[pixelIndex] = depth;

                            int r = Clamp(colorA.R * weightA + colorB.R * weightB + colorC.R * weightC);
                            int g = Clamp(colorA.G * weightA + colorB.G * weightB + colorC.G * weightC);
                            int b = Clamp(colorA.B * weightA + colorB.B * weightB + colorC.B * weightC);
                            pixels[pixelIndex] = unchecked((int)0xFF000000) | (r << 16) | (g << 8) | b;
                        });
                }
            }

            using var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try
            {
                Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
            graphics.DrawImageUnscaled(bitmap, 0, 0);
        }

        private static Color ShadeCorner(Polyhedron polyhedron, Polygon face, int cornerIndex, int vertexIndex, Camera camera)
        {
            Point3D normal = polyhedron.GetCornerNormal(face, cornerIndex);
            return Lighting.Lambert(normal, polyhedron.Vertices[vertexIndex], camera.LightPosition, camera.ObjectColor);
        }

        private static bool IsValidVertex(int index, bool[] valid) => index >= 0 && index < valid.Length && valid[index];

        private static int Clamp(double value)
        {
            if (value <= 0) return 0;
            if (value >= 255) return 255;
            return (int)value;
        }
    }
}
