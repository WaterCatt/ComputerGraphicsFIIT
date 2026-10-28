using System;
using System.Collections.Generic;

namespace Lab6
{
    public partial class Polyhedron
    {
        public List<Point3D> Vertices;
        public List<Polygon> Faces;
        public HashSet<Tuple<int, int>> Edges;

        public Polyhedron()
        {
            Vertices = new List<Point3D>();
            Faces = new List<Polygon>();
            Edges = new HashSet<Tuple<int, int>>();
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
            if (Vertices.Count == 0) return new Point3D(0, 0, 0);

            double x = 0;
            double y = 0;
            double z = 0;

            foreach (var v in Vertices)
            {
                x += v.X; 
                y += v.Y; 
                z += v.Z;
            }

            return new Point3D(x/Vertices.Count, y/Vertices.Count, z/Vertices.Count);
        }
    }
}