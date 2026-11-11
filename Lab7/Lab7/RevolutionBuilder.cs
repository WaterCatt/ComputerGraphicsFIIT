using Lab7;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Lab7
{
    public static class RevolutionBuilder
    {
        public static Polyhedron Build(List<Point3D> points, string axisName, int parts)
        {
            if (points == null || points.Count < 2)
            {
                throw new ArgumentException("Образующая должна содержать хотя бы две точки.");
            }

            if (parts < 3)
            {
                parts = 3;
            }

            Point3D axis = GetAxis(axisName);
            Polyhedron pol = new Polyhedron();

            int count = points.Count;
            double step = 2 * Math.PI / parts;

            for (int i = 0; i < parts; i++)
            {
                double angle = i * step;
                double[,] m = MatrixOps.Rotate(angle, axis);

                foreach (Point3D p in points)
                {
                    double x = p.X * m[0, 0] + p.Y * m[0, 1] + p.Z * m[0, 2] + m[0, 3];
                    double y = p.X * m[1, 0] + p.Y * m[1, 1] + p.Z * m[1, 2] + m[1, 3];
                    double z = p.X * m[2, 0] + p.Y * m[2, 1] + p.Z * m[2, 2] + m[2, 3];

                    pol.Vertices.Add(new Point3D(x, y, z));
                }
            }

            for (int i = 0; i < parts; i++)
            {
                int next = (i + 1) % parts;

                for (int j = 0; j < count - 1; j++)
                {
                    int a = i * count + j;
                    int b = i * count + j + 1;
                    int c = next * count + j + 1;
                    int d = next * count + j;

                    pol.Faces.Add(new Polygon(new List<int> { a, b, c, d }));
                }
            }

            pol.BuildEdges();

            return pol;
        }

        private static Point3D GetAxis(string name)
        {
            if (name == null)
            {
                return new Point3D(0, 0, 1);
            }

            name = name.Trim().ToUpper();

            if (name == "X")
            {
                return new Point3D(1, 0, 0);
            }

            if (name == "Y")
            {
                return new Point3D(0, 1, 0);
            }

            return new Point3D(0, 0, 1);
        }
    }
}