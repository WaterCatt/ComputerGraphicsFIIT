using System;
using System.Collections.Generic;

namespace Lab8
{
    public static class BackFaceCulling
    {
        public static HashSet<Tuple<int, int>> GetVisibleEdges(Polyhedron polyhedron, Camera camera)
        {
            var visibleEdges = new HashSet<Tuple<int, int>>();

            if (polyhedron == null || camera == null)
                return visibleEdges;

            //модели, заданные только рёбрами, нельзя отсечь по нормалям граней. возвращаем все рёбра
            if (polyhedron.Faces == null || polyhedron.Faces.Count == 0)
            {
                foreach (var edge in polyhedron.Edges)
                    visibleEdges.Add(NormalizeEdge(edge.Item1, edge.Item2));
                return visibleEdges;
            }

            Point3D objectCenter = polyhedron.GetCenter(); 
            Point3D parallelViewDirection = camera.Position - camera.LookAt; //вектор взгляда камеры
            parallelViewDirection.Normalize();

            foreach (var face in polyhedron.Faces) //перебираем все грани
            {
                if (face?.VertexIndices == null || face.VertexIndices.Count < 3) //если у грани нет верш или их <3 =>скип
                    continue;

                Point3D faceCenter = GetFaceCenter(polyhedron, face); //беру центр
                if (faceCenter == null)
                    continue;

                Point3D first = polyhedron.Vertices[face.VertexIndices[0]]; //беру 3 вершины чтобы задать плоскость
                Point3D second = polyhedron.Vertices[face.VertexIndices[1]];
                Point3D third = polyhedron.Vertices[face.VertexIndices[2]];

                Point3D normal = Point3D.Cross(second - first, third - first); //вектор нормали, перпеник грани
                if (normal.Length() <= 1e-9)
                    continue;

                //нормаль ориентируется наружу
                if (Point3D.Dot(normal, faceCenter - objectCenter) < 0)
                    normal = normal * -1;

                Point3D viewDirection = camera.Type == ProjectionType.Perspective //вектор обзора для конкретной грани
                    ? camera.Position - faceCenter
                    : parallelViewDirection;

                if (Point3D.Dot(normal, viewDirection) <= 1e-9) //если dot > 0 - грань смотрит на камеру(наша темка), <0 - не смотрит на нас(не наша темка), =0 - грань ребром к камере, всё, что не >0 скип
                    continue;

                for (int i = 0; i < face.VertexIndices.Count; i++) //идём по всем вершинам, замыкаем грань и добавляем её в результат
                {
                    int a = face.VertexIndices[i];
                    int b = face.VertexIndices[(i + 1) % face.VertexIndices.Count];
                    if (a >= 0 && a < polyhedron.Vertices.Count && b >= 0 && b < polyhedron.Vertices.Count)
                        visibleEdges.Add(NormalizeEdge(a, b));
                }
            }

            return visibleEdges;
        }

        private static Point3D GetFaceCenter(Polyhedron polyhedron, Polygon face) //находит центр масс. складывает коорд всех верш граней и делит на их кол-во
        {
            double x = 0, y = 0, z = 0;
            foreach (int index in face.VertexIndices)
            {
                if (index < 0 || index >= polyhedron.Vertices.Count)
                    return null;

                Point3D vertex = polyhedron.Vertices[index];
                x += vertex.X;
                y += vertex.Y;
                z += vertex.Z;
            }

            int count = face.VertexIndices.Count;
            return new Point3D(x / count, y / count, z / count);
        }

        private static Tuple<int, int> NormalizeEdge(int a, int b) //шоб в хэшсет не добавлять одно и то же
        {
            return a < b ? Tuple.Create(a, b) : Tuple.Create(b, a);
        }
    }
}
