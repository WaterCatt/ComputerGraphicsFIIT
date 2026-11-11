using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Lab8
{
    public static class ZBufferRenderer
    {
        public static void Draw(Graphics graphics, Polyhedron polyhedron, Camera camera, int width, int height, bool severalObjects)
        {

            double[] zBuffer = new double[width * height];
            int[] pixels = new int[width * height];

            for (int i = 0; i < zBuffer.Length; i++)
            {
                zBuffer[i] = double.PositiveInfinity;
            }

            if (severalObjects)
            {
                DrawObject(polyhedron, camera, new Point3D(-1, 0, 0), Color.Blue, width, height, zBuffer, pixels);
                DrawObject(polyhedron, camera, new Point3D(1, 0, 0), Color.Red, width, height, zBuffer, pixels);
            }
            else
            {
                DrawObject(polyhedron, camera, new Point3D(0, 0, 0), Color.Blue, width, height, zBuffer, pixels);
            }

            using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);

                bitmap.UnlockBits(data);
                graphics.DrawImageUnscaled(bitmap, 0, 0);
            }
        }

        private static void DrawObject(Polyhedron polyhedron, Camera camera, Point3D position, Color color, int width, int height, double[] zBuffer, int[] pixels)
        {
            double[,] move = MatrixOps.Translate(position.X, position.Y, position.Z);
            double[,] view = camera.GetViewMatrix();
            double[,] projection = camera.GetProjectionMatrix();

            double[,] matrix = MatrixOps.Multiply(projection, MatrixOps.Multiply(view, move));

            Point3D[] points = new Point3D[polyhedron.Vertices.Count];
            bool[] visible = new bool[polyhedron.Vertices.Count];

            float mult;

            if (camera.Type == ProjectionType.Perspective)
            {
                mult = Math.Min(width, height) / 2f;
            }
            else
            {
                mult = Math.Min(width, height) / (2f * 3.5f);
            }

            for (int i = 0; i < polyhedron.Vertices.Count; i++)
            {
                Point3D p = polyhedron.Vertices[i];

                double x = p.X * matrix[0, 0] + p.Y * matrix[0, 1] + p.Z * matrix[0, 2] + p.W * matrix[0, 3];
                double y = p.X * matrix[1, 0] + p.Y * matrix[1, 1] + p.Z * matrix[1, 2] + p.W * matrix[1, 3];
                double z = p.X * matrix[2, 0] + p.Y * matrix[2, 1] + p.Z * matrix[2, 2] + p.W * matrix[2, 3];
                double w = p.X * matrix[3, 0] + p.Y * matrix[3, 1] + p.Z * matrix[3, 2] + p.W * matrix[3, 3];

                if (camera.Type == ProjectionType.Perspective)
                {
                    if (w <= 0.05)
                    {
                        continue;
                    }

                    x /= w;
                    y /= w;
                    z /= w;
                }
                else
                {
                    z = -z;
                }

                double screenX = width / 2.0 + x * mult;
                double screenY = height / 2.0 - y * mult;

                points[i] = new Point3D(screenX, screenY, z);
                visible[i] = true;
            }

            foreach (Polygon face in polyhedron.Faces)
            {
                if (face.VertexIndices.Count < 3)
                {
                    continue;
                }

                int a = face.VertexIndices[0];

                for (int i = 1; i < face.VertexIndices.Count - 1; i++)
                {
                    int b = face.VertexIndices[i];
                    int c = face.VertexIndices[i + 1];

                    if (!visible[a] || !visible[b] || !visible[c])
                    {
                        continue;
                    }

                    DrawTriangle(points[a], points[b], points[c], color, width, height, zBuffer, pixels);
                }
            }
        }

        private static void DrawTriangle(Point3D a, Point3D b, Point3D c, Color color, int width, int height, double[] zBuffer, int[] pixels)
        {
            double area = Edge(a, b, c.X, c.Y);

            if (Math.Abs(area) < 0.000001)
            {
                return;
            }

            int minX = Math.Max(0, (int)Math.Floor(Math.Min(a.X, Math.Min(b.X, c.X))));
            int maxX = Math.Min(width - 1, (int)Math.Ceiling(Math.Max(a.X, Math.Max(b.X, c.X))));
            int minY = Math.Max(0, (int)Math.Floor(Math.Min(a.Y, Math.Min(b.Y, c.Y))));
            int maxY = Math.Min(height - 1, (int)Math.Ceiling(Math.Max(a.Y, Math.Max(b.Y, c.Y))));

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    double px = x + 0.5;
                    double py = y + 0.5;

                    double k1 = Edge(b, c, px, py) / area;
                    double k2 = Edge(c, a, px, py) / area;
                    double k3 = Edge(a, b, px, py) / area;

                    if (k1 < 0 || k2 < 0 || k3 < 0)
                    {
                        continue;
                    }

                    double z = k1 * a.Z + k2 * b.Z + k3 * c.Z;
                    int index = y * width + x;

                    if (z < zBuffer[index])
                    {
                        zBuffer[index] = z;
                        pixels[index] = color.ToArgb();
                    }
                }
            }
        }

        private static double Edge(Point3D a, Point3D b, double x, double y)
        {
            return (b.X - a.X) * (y - a.Y) - (b.Y - a.Y) * (x - a.X);
        }
    }
}