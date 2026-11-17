using System;

namespace Lab9
{
    public partial class Polyhedron
    {
        public void ApplyTransformation(double[,] matrix)
        {
            TransformImportedNormals(matrix);

            for (int i = 0; i < Vertices.Count; i++)
            {
                var v = Vertices[i];
                double x = v.X * matrix[0, 0] + v.Y * matrix[0, 1] + v.Z * matrix[0, 2] + v.W * matrix[0, 3];
                double y = v.X * matrix[1, 0] + v.Y * matrix[1, 1] + v.Z * matrix[1, 2] + v.W * matrix[1, 3];
                double z = v.X * matrix[2, 0] + v.Y * matrix[2, 1] + v.Z * matrix[2, 2] + v.W * matrix[2, 3];
                double w = v.X * matrix[3, 0] + v.Y * matrix[3, 1] + v.Z * matrix[3, 2] + v.W * matrix[3, 3];
                if (Math.Abs(w) > 1e-9) 
                { 
                    x /= w; 
                    y /= w; 
                    z /= w; 
                }
                Vertices[i] = new Point3D(x, y, z);
            }

            if (VertexNormals.Count == Vertices.Count)
                ComputeVertexNormals();
            else
                ComputeFaceNormals();
        }

        private void TransformImportedNormals(double[,] matrix)
        {
            if (ImportedNormals.Count == 0)
                return;

            double a = matrix[0, 0], b = matrix[0, 1], c = matrix[0, 2];
            double d = matrix[1, 0], e = matrix[1, 1], f = matrix[1, 2];
            double g = matrix[2, 0], h = matrix[2, 1], i = matrix[2, 2];
            double det = a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);
            if (Math.Abs(det) <= 1e-12)
                return;

            //обратная транспонированная матрица: алг. дополнение матрицы / определитель
            double[,] normalMatrix = {
                { (e * i - f * h) / det, (f * g - d * i) / det, (d * h - e * g) / det },
                { (c * h - b * i) / det, (a * i - c * g) / det, (b * g - a * h) / det },
                { (b * f - c * e) / det, (c * d - a * f) / det, (a * e - b * d) / det }
            };

            foreach (Point3D normal in ImportedNormals)
            {
                double x = normal.X, y = normal.Y, z = normal.Z;
                normal.X = normalMatrix[0, 0] * x + normalMatrix[0, 1] * y + normalMatrix[0, 2] * z;
                normal.Y = normalMatrix[1, 0] * x + normalMatrix[1, 1] * y + normalMatrix[1, 2] * z;
                normal.Z = normalMatrix[2, 0] * x + normalMatrix[2, 1] * y + normalMatrix[2, 2] * z;
                normal.Normalize();
            }
        }

        public void Translate(double tx, double ty, double tz)
        {
            ApplyTransformation(MatrixOps.Translate(tx, ty, tz));
        }

        public void ScaleAroundCenter(double tx, double ty, double tz)
        {
            var c = GetCenter();
            ApplyTransformation(MatrixOps.Translate(-c.X, -c.Y, -c.Z));
            ApplyTransformation(MatrixOps.Scale(tx, ty, tz));
            ApplyTransformation(MatrixOps.Translate(c.X, c.Y, c.Z));
        }

        public void RotateAroundCenterAxis(string axisName, double angle)
        {
            Point3D axis;
            if (axisName == "X") 
                axis = new Point3D(1, 0, 0);
            else if (axisName == "Y") 
                axis = new Point3D(0, 1, 0);
            else 
                axis = new Point3D(0, 0, 1);

            Point3D c = GetCenter();
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
            Point3D normal;
            if (planeName == "XY") 
                normal = new Point3D(0, 0, 1);
            else if (planeName == "XZ") 
                normal = new Point3D(0, 1, 0);
            else 
                normal = new Point3D(1, 0, 0);
            ApplyTransformation(MatrixOps.Reflect(normal, new Point3D(0, 0, 0)));
        }
    }
}
