using System;
using System.Collections.Generic;

namespace Lab9
{
    public static class Culling
    {
        public static bool IsFaceVisible(Polyhedron polyhedron, int faceIndex, Camera camera)
        {
            if (polyhedron == null || camera == null)
                return false;
            if (polyhedron.Faces == null || faceIndex < 0 || faceIndex >= polyhedron.Faces.Count)
                return false;

            var face = polyhedron.Faces[faceIndex];
            if (face == null || face.VertexIndices == null || face.VertexIndices.Count < 3)
                return false;

            Point3D faceCenter = polyhedron.GetFaceCenter(face);
            Point3D normal = GetOrientedNormal(polyhedron, faceIndex, faceCenter);
            if (normal.Length() <= 1e-9)
                return false;

            Point3D viewDirection;
            if (camera.Type == ProjectionType.Perspective)
                viewDirection = camera.Position - faceCenter;
            else
            {
                viewDirection = camera.Position - camera.LookAt;
                viewDirection.Normalize();
            }

            return Point3D.Dot(normal, viewDirection) > 1e-9;
        }

        public static Point3D GetOrientedNormal(Polyhedron polyhedron, int faceIndex, Point3D faceCenter)
        {
            var face = polyhedron.Faces[faceIndex];

            if (polyhedron.FaceNormals != null && polyhedron.FaceNormals.Count > faceIndex)
            {
                Point3D cached = polyhedron.FaceNormals[faceIndex];
                if (cached.Length() > 1e-9)
                    return cached;
            }

            Point3D first = polyhedron.Vertices[face.VertexIndices[0]];
            Point3D second = polyhedron.Vertices[face.VertexIndices[1]];
            Point3D third = polyhedron.Vertices[face.VertexIndices[2]];

            Point3D normal = Point3D.Cross(second - first, third - first);
            if (normal.Length() <= 1e-9)
                return new Point3D(0, 0, 0);

            Point3D center = polyhedron.GetCenter();
            if (Point3D.Dot(normal, faceCenter - center) < 0)
                normal = normal * -1;
            normal.Normalize();
            return normal;
        }

        public static HashSet<Tuple<int, int>> GetVisibleEdges(Polyhedron polyhedron, Camera camera)
        {
            var visibleEdges = new HashSet<Tuple<int, int>>();

            if (polyhedron == null || camera == null)
                return visibleEdges;

            if (polyhedron.Faces == null || polyhedron.Faces.Count == 0)
            {
                foreach (var edge in polyhedron.Edges)
                    visibleEdges.Add(NormalizeEdge(edge.Item1, edge.Item2));
                return visibleEdges;
            }

            for (int f = 0; f < polyhedron.Faces.Count; f++)
            {
                if (!IsFaceVisible(polyhedron, f, camera))
                    continue;

                var face = polyhedron.Faces[f];
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

        private static Tuple<int, int> NormalizeEdge(int a, int b)
        {
            return a < b ? Tuple.Create(a, b) : Tuple.Create(b, a);
        }
    }
}