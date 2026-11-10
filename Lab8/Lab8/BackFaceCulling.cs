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

            // Модели, заданные только рёбрами, нельзя отсечь по нормалям граней.
            if (polyhedron.Faces == null || polyhedron.Faces.Count == 0)
            {
                foreach (var edge in polyhedron.Edges)
                    visibleEdges.Add(NormalizeEdge(edge.Item1, edge.Item2));
                return visibleEdges;
            }

            Point3D objectCenter = polyhedron.GetCenter();
            Point3D parallelViewDirection = camera.Position - camera.LookAt;
            parallelViewDirection.Normalize();

            foreach (var face in polyhedron.Faces)
            {
                if (face?.VertexIndices == null || face.VertexIndices.Count < 3)
                    continue;

                Point3D faceCenter = GetFaceCenter(polyhedron, face);
                if (faceCenter == null)
                    continue;

                Point3D first = polyhedron.Vertices[face.VertexIndices[0]];
                Point3D second = polyhedron.Vertices[face.VertexIndices[1]];
                Point3D third = polyhedron.Vertices[face.VertexIndices[2]];

                Point3D normal = Point3D.Cross(second - first, third - first);
                if (normal.Length() <= 1e-9)
                    continue;

                // Не полагаемся на порядок вершин грани: нормаль ориентируется наружу.
                if (Point3D.Dot(normal, faceCenter - objectCenter) < 0)
                    normal = normal * -1;

                Point3D viewDirection = camera.Type == ProjectionType.Perspective
                    ? camera.Position - faceCenter
                    : parallelViewDirection;

                if (Point3D.Dot(normal, viewDirection) <= 1e-9)
                    continue;

                for (int i = 0; i < face.VertexIndices.Count; i++)
                {
                    int a = face.VertexIndices[i];
                    int b = face.VertexIndices[(i + 1) % face.VertexIndices.Count];
                    if (a >= 0 && a < polyhedron.Vertices.Count && b >= 0 && b < polyhedron.Vertices.Count)
                        visibleEdges.Add(NormalizeEdge(a, b));
                }
            }

            return visibleEdges;
        }

        private static Point3D GetFaceCenter(Polyhedron polyhedron, Polygon face)
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

        private static Tuple<int, int> NormalizeEdge(int a, int b)
        {
            return a < b ? Tuple.Create(a, b) : Tuple.Create(b, a);
        }
    }
}
