using System;

namespace Lab6
{
    public partial class Polyhedron
    {
        public void ApplyTransformation(double[,] matrix)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                var v = Vertices[i];
                double x = v.X * matrix[0, 0] + v.Y * matrix[0, 1] + v.Z * matrix[0, 2] + v.W * matrix[0, 3];
                double y = v.X * matrix[1, 0] + v.Y * matrix[1, 1] + v.Z * matrix[1, 2] + v.W * matrix[1, 3];
                double z = v.X * matrix[2, 0] + v.Y * matrix[2, 1] + v.Z * matrix[2, 2] + v.W * matrix[2, 3];
                double w = v.X * matrix[3, 0] + v.Y * matrix[3, 1] + v.Z * matrix[3, 2] + v.W * matrix[3, 3];

                if (Math.Abs(w) > 1e-9 && Math.Abs(w - 1.0) > 1e-9)
                    x /= w; y /= w; z /= w;
                Vertices[i] = new Point3D(x, y, z);
            }
        }

        public void Translate(double tx, double ty, double tz)
        {
            ApplyTransformation(MatrixOps.Translate(tx, ty, tz));
        }

        public void ScaleAroundCenter(double sx, double sy, double sz)
        {
            var c = GetGcenter();
            ApplyTransformation(MatrixOps.Translate(-c.X, -c.Y, -c.Z));
            ApplyTransformation(MatrixOps.Scale(sx, sy, sz));
            ApplyTransformation(MatrixOps.Translate(c.X, c.Y, c.Z));
        }

        public void RotateAroundCenterAxis(string axisName, double angle)
        {
            Point3D axis = axisName.ToUpper() switch
            {
                "X" => new Point3D(1, 0, 0),
                "Y" => new Point3D(0, 1, 0),
                "Z" => new Point3D(0, 0, 1),
                _ => throw new ArgumentException("Неверная ось")
            };

            var c = GetGcenter();
            ApplyTransformation(MatrixOps.Translate(-c.X, -c.Y, -c.Z));
            ApplyTransformation(MatrixOps.Rotate(angle, axis));
            ApplyTransformation(MatrixOps.Translate(c.X, c.Y, c.Z));
        }

        public void RotateAroundLine(Point3D p1, Point3D p2, double angle)
        {
            Point3D dir = p2 - p1;
            ApplyTransformation(MatrixOps.Translate(-p1.X, -p1.Y, -p1.Z));
            ApplyTransformation(MatrixOps.Rotate(angle, dir));
            ApplyTransformation(MatrixOps.Translate(p1.X, p1.Y, p1.Z));
        }

        public void ReflectCoordinatePlane(string planeName)
        {
            Point3D normal = planeName.ToUpper() switch
            {
                "XY" => new Point3D(0, 0, 1),
                "XZ" => new Point3D(0, 1, 0),
                "YZ" => new Point3D(1, 0, 0),
                _ => throw new ArgumentException("Неверная плоскость")
            };
            ApplyTransformation(MatrixOps.Reflect(normal, new Point3D(0, 0, 0)));
        }
    }
}