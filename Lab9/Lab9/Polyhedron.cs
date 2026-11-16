using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab9
{
    public partial class Polyhedron
    {
        public List<Point3D> Vertices;
        public List<Polygon> Faces;
        public HashSet<Tuple<int, int>> Edges;

        public List<Point3D> VertexNormals;
        public List<Point3D> FaceNormals;
        public List<PointF> TexCoords;

        public Polyhedron()
        {
            Vertices = new List<Point3D>();
            Faces = new List<Polygon>();
            Edges = new HashSet<Tuple<int, int>>();
            VertexNormals = new List<Point3D>();
            FaceNormals = new List<Point3D>();
            TexCoords = new List<PointF>();
        }

        public void BuildEdges()
        {
            Edges.Clear();
            foreach (var face in Faces)
            {
                int count = face.VertexIndices.Count;
                for (int i = 0; i < count; i++)
                {
                    int v1 = face.VertexIndices[i];
                    int v2 = face.VertexIndices[(i + 1) % count];
                    var edge = v1 < v2 ? Tuple.Create(v1, v2) : Tuple.Create(v2, v1);
                    Edges.Add(edge);
                }
            }
        }

        public Point3D GetCenter()
        {
            if (Vertices.Count == 0) 
                return new Point3D(0, 0, 0);
            double x = 0, y = 0, z = 0;
            foreach (var v in Vertices)
            {
                x += v.X; 
                y += v.Y; 
                z += v.Z;
            }
            return new Point3D(x / Vertices.Count, y / Vertices.Count, z / Vertices.Count);
        }

        public void ComputeFaceNormals()
        {
            FaceNormals.Clear();
            Point3D center = GetCenter();
            for (int f = 0; f < Faces.Count; f++)
            {
                var face = Faces[f];
                if (face.VertexIndices.Count < 3)
                {
                    FaceNormals.Add(new Point3D(0, 0, 0));
                    continue;
                }

                Point3D first = Vertices[face.VertexIndices[0]];
                Point3D second = Vertices[face.VertexIndices[1]];
                Point3D third = Vertices[face.VertexIndices[2]];

                Point3D normal = Point3D.Cross(second - first, third - first);
                if (normal.Length() > 1e-9)
                {
                    Point3D fc = GetFaceCenter(face);
                    if (Point3D.Dot(normal, fc - center) < 0)
                        normal = normal * -1;
                    normal.Normalize();
                }
                else
                    normal = new Point3D(0, 0, 0);
                FaceNormals.Add(normal);
            }
        }

        public void ComputeVertexNormals()
        {
            if (Vertices.Count == 0)
                return;

            ComputeFaceNormals();

            var accum = new Point3D[Vertices.Count];
            for (int i = 0; i < accum.Length; i++)
                accum[i] = new Point3D(0, 0, 0);

            for (int f = 0; f < Faces.Count; f++)
            {
                var face = Faces[f];
                if (FaceNormals.Count <= f) 
                    break;
                Point3D fn = FaceNormals[f];
                foreach (int idx in face.VertexIndices)
                {
                    if (idx < 0 || idx >= accum.Length) 
                        continue;
                    accum[idx] = accum[idx] + fn;
                }
            }

            VertexNormals.Clear();
            for (int i = 0; i < accum.Length; i++)
            {
                Point3D n = accum[i];
                if (n.Length() > 1e-9) 
                    n.Normalize();
                else n = new Point3D(0, 0, 1);
                VertexNormals.Add(n);
            }
        }

        public Point3D GetFaceCenter(Polygon face)
        {
            double x = 0, y = 0, z = 0;
            foreach (int index in face.VertexIndices)
            {
                Point3D v = Vertices[index];
                x += v.X; y += v.Y; z += v.Z;
            }
            int count = face.VertexIndices.Count;
            return new Point3D(x / count, y / count, z / count);
        }
    }
}