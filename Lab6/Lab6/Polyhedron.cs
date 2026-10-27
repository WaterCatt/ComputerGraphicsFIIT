using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6
{
    public partial class Polyhedron
    {
        public List<Point3D> Vertices { get; set; }
        public List<Polygon> Faces { get; set; }
        public List<Tuple<int, int>> Edges { get; set; }

        public Polyhedron()
        {
            Vertices = new List<Point3D>();
            Faces = new List<Polygon>();
            Edges = new List<Tuple<int, int>>();
        }

        public Polyhedron Copy()
        {
            var copy = new Polyhedron();
            copy.Vertices = Vertices.Select(v => v.Copy()).ToList();
            copy.Faces = Faces.Select(f => f.Copy()).ToList();
            copy.Edges = new List<Tuple<int, int>>(Edges);
            return copy;
        }

        public void BuildEdges()
        {
            Edges.Clear();
            var edgeSet = new HashSet<Tuple<int, int>>();

            foreach (var face in Faces)
            {
                int count = face.VertexIndices.Count;
                for (int i = 0; i < count; i++)
                {
                    int v1 = face.VertexIndices[i];
                    int v2 = face.VertexIndices[(i + 1) % count];

                    var edge = v1 < v2 ? Tuple.Create(v1, v2) : Tuple.Create(v2, v1);
                    edgeSet.Add(edge);
                }
            }

            Edges.AddRange(edgeSet);
        }

        public Point3D GetGcenter()
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
            return new Point3D(x/Vertices.Count, y/Vertices.Count, z/Vertices.Count);
        }
    }
}