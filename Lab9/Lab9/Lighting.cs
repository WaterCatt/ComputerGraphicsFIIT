using System;
using System.Drawing;

namespace Lab9
{
    public static class Lighting
    {
        public static Color Lambert(Point3D normal, Point3D worldPos, Point3D lightPos, Color objectColor)
        {
            if (normal.Length() <= 1e-9)
                return Color.Black;

            Point3D n = normal.Copy();
            n.Normalize();

            Point3D toLight = lightPos - worldPos;
            if (toLight.Length() <= 1e-9)
                return Color.Black;
            toLight.Normalize();

            double diffuse = Math.Max(0.0, Point3D.Dot(n, toLight));
            const double ambient = 0.15;
            double intensity = ambient + (1.0 - ambient) * diffuse;

            int r = Clamp(objectColor.R * intensity);
            int g = Clamp(objectColor.G * intensity);
            int b = Clamp(objectColor.B * intensity);
            return Color.FromArgb(r, g, b);
        }

        public static Color LambertBarycentric(
            Point3D n1, Point3D n2, Point3D n3,
            Point3D p1, Point3D p2, Point3D p3,
            double k1, double k2, double k3,
            Point3D lightPos, Color objectColor)
        {
            Point3D normal = n1 * k1 + n2 * k2 + n3 * k3;
            Point3D pos = p1 * k1 + p2 * k2 + p3 * k3;
            return Lambert(normal, pos, lightPos, objectColor);
        }

        private static int Clamp(double v)
        {
            if (v < 0) 
                return 0;
            if (v > 255) 
                return 255;
            return (int)v;
        }

        public static int PackColor(Color c)
        {
            return (c.A << 24) | (c.R << 16) | (c.G << 8) | c.B;
        }
    }
}