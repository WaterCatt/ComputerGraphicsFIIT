using Lab8;
using System;
using System.Collections.Generic;

namespace Lab8
{
    public static class SurfaceBuilder
    {
        public static Polyhedron Build(Func<double, double, double> f, double x0, double x1, double y0, double y1, int nx, int ny) //делегат, границы, колво разбиений
        {
            if (f == null) //защита от дурака
                throw new ArgumentNullException(nameof(f));
            if (nx < 1)
                nx = 1;
            if (ny < 1)
                ny = 1;
            if (x1 == x0)
                x1 = x0 + 1;
            if (y1 == y0)
                y1 = y0 + 1;

            var polyhedron = new Polyhedron();
            double dx = (x1 - x0) / nx; //расст между соседними точками
            double dy = (y1 - y0) / ny;

            for (int i = 0; i <= nx; i++) //генрация вершин
            {
                double x = x0 + i * dx;
                for (int j = 0; j <= ny; j++)
                {
                    double y = y0 + j * dy;
                    double z = f(x, y);
                    polyhedron.Vertices.Add(new Point3D(x, y, z));
                }
            }

            int row = ny + 1;
            for (int i = 0; i < nx; i++) //генерация ребер
            {
                for (int j = 0; j < ny; j++)
                {
                    int v0 = i * row + j; //левый низ
                    int v1 = v0 + 1; //правый низ
                    int v2 = (i + 1) * row + j + 1; //левый верх (след слобец)
                    int v3 = (i + 1) * row + j; // прав верх

                    polyhedron.Faces.Add(new Polygon(new List<int> { v0, v1, v2, v3 }));
                }
            }

            polyhedron.BuildEdges();
            return polyhedron;
        }
    }
}