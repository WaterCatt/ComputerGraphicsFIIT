using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Lab9
{
    public static class PhongRenderer
    {
        private static Color Toon(Point3D normal, Point3D worldPos, Point3D lightPos, Color objectColor, int levels)
        {
            if (normal.Length() <= 1e-9 || levels < 1)
                return Color.Black;

            Point3D n = normal.Copy();
            n.Normalize();

            Point3D toLight = lightPos - worldPos;
            if (toLight.Length() <= 1e-9)
                return Color.Black;
            toLight.Normalize();

            double diffuse = Math.Max(0.0, Point3D.Dot(n, toLight));
            double quantized = Math.Floor(diffuse * levels) / levels;
            const double ambient = 0.25;
            double intensity = ambient + (1.0 - ambient) * quantized;

            return Color.FromArgb(Clamp(objectColor.R * intensity), Clamp(objectColor.G * intensity), Clamp(objectColor.B * intensity));
        }

        private static int Clamp(double v)
        {
            if (v < 0) 
                return 0;
            if (v > 255) 
                return 255;
            return (int)v;
        }

        public static void Draw(Graphics g, Polyhedron p, Camera c, int w, int h)
        {
            Rasterizer.ResetBuffers(w, h, out double[] zBuffer, out int[] pixels);

            double[,] mvp = MatrixOps.Multiply(c.GetProjectionMatrix(), c.GetViewMatrix());
            int n = p.Vertices.Count;
            var screen = new PointF[n];
            var world = new Point3D[n];
            var depth = new double[n];
            var invW = new double[n];
            var valid = new bool[n];
            float mult = c.Type == ProjectionType.Perspective ? Math.Min(w, h) / 2f : Math.Min(w, h) / (2f * 3.5f);

            for (int i = 0; i < n; i++)
            {
                Point3D v = p.Vertices[i];
                world[i] = v;

                double x = v.X * mvp[0, 0] + v.Y * mvp[0, 1] + v.Z * mvp[0, 2] + v.W * mvp[0, 3];
                double y = v.X * mvp[1, 0] + v.Y * mvp[1, 1] + v.Z * mvp[1, 2] + v.W * mvp[1, 3];
                double z = v.X * mvp[2, 0] + v.Y * mvp[2, 1] + v.Z * mvp[2, 2] + v.W * mvp[2, 3];
                double ww = v.X * mvp[3, 0] + v.Y * mvp[3, 1] + v.Z * mvp[3, 2] + v.W * mvp[3, 3];

                if (c.Type == ProjectionType.Perspective)
                {
                    if (ww <= 0.05)
                    { 
                        valid[i] = false; 
                        continue; 
                    }
                    invW[i] = 1.0 / ww;
                    depth[i] = z / ww;
                    x /= ww;
                    y /= ww;
                }
                else
                {
                    invW[i] = 1.0;
                    depth[i] = -z;
                }

                screen[i] = new PointF((float)(w / 2.0 + x * mult), (float)(h / 2.0 - y * mult));
                valid[i] = true;
            }

            if (p.VertexNormals.Count != p.Vertices.Count)
                p.ComputeVertexNormals();

            Point3D lightPos = c.LightPosition;
            Color baseColor = c.ObjectColor;
            const int toonLevels = 4;

            foreach (var face in p.Faces)
            {
                if (face.VertexIndices.Count < 3) 
                    continue;
                int ia = face.VertexIndices[0];
                for (int k = 1; k < face.VertexIndices.Count - 1; k++)
                {
                    int ib = face.VertexIndices[k];
                    int ic = face.VertexIndices[k + 1];
                    if (!valid[ia] || !valid[ib] || !valid[ic])
                        continue;

                    Point3D sa = new Point3D(screen[ia].X, screen[ia].Y, depth[ia]);
                    Point3D sb = new Point3D(screen[ib].X, screen[ib].Y, depth[ib]);
                    Point3D sc = new Point3D(screen[ic].X, screen[ic].Y, depth[ic]);

                    Point3D na = p.GetCornerNormal(face, 0);
                    Point3D nb = p.GetCornerNormal(face, k);
                    Point3D nc = p.GetCornerNormal(face, k + 1);
                    Point3D wa = world[ia], wb = world[ib], wc = world[ic];
                    double iwA = invW[ia], iwB = invW[ib], iwC = invW[ic];

                    Rasterizer.FillTriangle(sa, sb, sc, w, h, (index, k1, k2, k3, z) =>
                    {
                        if (z >= zBuffer[index])
                            return;
                        zBuffer[index] = z;

                        double wA = k1 * iwA, wB = k2 * iwB, wC = k3 * iwC;
                        double wSum = wA + wB + wC;
                        if (wSum <= 1e-12) 
                            return;
                        double aW = wA / wSum, bW = wB / wSum, cW = wC / wSum;

                        Point3D nI = na * aW + nb * bW + nc * cW;
                        nI.Normalize();
                        Point3D pI = wa * aW + wb * bW + wc * cW;

                        Color col = Toon(nI, pI, lightPos, baseColor, toonLevels);
                        pixels[index] = (255 << 24) | (col.R << 16) | (col.G << 8) | col.B;
                    });
                }
            }

            using var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            var data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
            bmp.UnlockBits(data);
            g.DrawImageUnscaled(bmp, 0, 0);
        }
    }
}
