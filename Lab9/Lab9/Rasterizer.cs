using System;
using System.Drawing;

namespace Lab9
{
    public delegate int ShadeFunc(double k1, double k2, double k3, double z);

    public static class Rasterizer
    {
        public static void FillTriangle(Point3D a, Point3D b, Point3D c, int width, int height, double[] zBuffer, int[] pixels, ShadeFunc shade)
        {
            double area = Edge(a, b, c.X, c.Y);
            if (Math.Abs(area) < 1e-6)
                return;

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
                        continue;

                    double z = k1 * a.Z + k2 * b.Z + k3 * c.Z;
                    int index = y * width + x;
                    if (z < zBuffer[index])
                    {
                        zBuffer[index] = z;
                        pixels[index] = shade(k1, k2, k3, z);
                    }
                }
            }
        }

        public static void FillTriangleWithWeights(Point3D a, Point3D b, Point3D c, int width, int height, Action<int, double, double, double, double> onPixel)
        {
            double area = Edge(a, b, c.X, c.Y);
            if (Math.Abs(area) < 1e-6)
                return;

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
                        continue;

                    double z = k1 * a.Z + k2 * b.Z + k3 * c.Z;
                    int index = y * width + x;
                    onPixel(index, k1, k2, k3, z);
                }
            }
        }

        public static double Edge(Point3D a, Point3D b, double x, double y)
        {
            return (b.X - a.X) * (y - a.Y) - (b.Y - a.Y) * (x - a.X);
        }

        public static void ResetBuffers(int width, int height, out double[] zBuffer, out int[] pixels)
        {
            zBuffer = new double[width * height];
            pixels = new int[width * height];
            for (int i = 0; i < zBuffer.Length; i++)
                zBuffer[i] = double.PositiveInfinity;
        }
    }
}