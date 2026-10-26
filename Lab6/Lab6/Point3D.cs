using System;

namespace Lab6
{
    public class Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public Point3D(double x, double y, double z, double w = 1.0)
        {
            X = x; 
            Y = y; 
            Z = z; 
            W = w;
        }

        public Point3D Copy() => new Point3D(X, Y, Z, W);

        public static Point3D operator +(Point3D a, Point3D b) => new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Point3D operator -(Point3D a, Point3D b) => new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Point3D operator *(Point3D p, double scalar) => new Point3D(p.X * scalar, p.Y * scalar, p.Z * scalar);
        public static Point3D operator /(Point3D p, double scalar) => new Point3D(p.X / scalar, p.Y / scalar, p.Z / scalar);

        public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);

        public void Normalize()
        {
            double len = Length();
            if (len > 1e-9)
                X /= len; Y /= len; Z /= len;
        }

        public static double Dot(Point3D a, Point3D b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Point3D Cross(Point3D a, Point3D b) => new Point3D(
            a.Y * b.Z - a.Z * b.Y,
            a.Z * b.X - a.X * b.Z,
            a.X * b.Y - a.Y * b.X
        );
    }
}