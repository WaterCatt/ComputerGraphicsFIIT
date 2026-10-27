using System.Collections.Generic;
using System.Linq;
namespace Lab6
{
    public static class PolyhedronFactory
    {
        public static Polyhedron CreateIcosahedron()
        {
            var p = new Polyhedron();
            double t = (1.0 + System.Math.Sqrt(5.0)) / 2.0;

            p.Vertices.Add(new Point3D(-1, t, 0));
            p.Vertices.Add(new Point3D(1, t, 0));
            p.Vertices.Add(new Point3D(-1, -t, 0));
            p.Vertices.Add(new Point3D(1, -t, 0));

            p.Vertices.Add(new Point3D(0, -1, t));
            p.Vertices.Add(new Point3D(0, 1, t));
            p.Vertices.Add(new Point3D(0, -1, -t));
            p.Vertices.Add(new Point3D(0, 1, -t));

            p.Vertices.Add(new Point3D(t, 0, -1));
            p.Vertices.Add(new Point3D(t, 0, 1));
            p.Vertices.Add(new Point3D(-t, 0, -1));
            p.Vertices.Add(new Point3D(-t, 0, 1));

            p.Faces.Add(new Polygon(new List<int> { 0, 11, 5 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 5, 1 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 1, 7 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 7, 10 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 10, 11 }));

            p.Faces.Add(new Polygon(new List<int> { 1, 5, 9 }));
            p.Faces.Add(new Polygon(new List<int> { 5, 11, 4 }));
            p.Faces.Add(new Polygon(new List<int> { 11, 10, 2 }));
            p.Faces.Add(new Polygon(new List<int> { 10, 7, 6 }));
            p.Faces.Add(new Polygon(new List<int> { 7, 1, 8 }));

            p.Faces.Add(new Polygon(new List<int> { 3, 9, 4 }));
            p.Faces.Add(new Polygon(new List<int> { 3, 4, 2 }));
            p.Faces.Add(new Polygon(new List<int> { 3, 2, 6 }));
            p.Faces.Add(new Polygon(new List<int> { 3, 6, 8 }));
            p.Faces.Add(new Polygon(new List<int> { 3, 8, 9 }));

            p.Faces.Add(new Polygon(new List<int> { 4, 9, 5 }));
            p.Faces.Add(new Polygon(new List<int> { 2, 4, 11 }));
            p.Faces.Add(new Polygon(new List<int> { 6, 2, 10 }));
            p.Faces.Add(new Polygon(new List<int> { 8, 6, 7 }));
            p.Faces.Add(new Polygon(new List<int> { 9, 8, 1 }));

            p.BuildEdges();
            return p;
        }

        public static Polyhedron CreateDodecahedron()
        {
            var ico = CreateIcosahedron();
            var dodeca = new Polyhedron();

            var faceCenters = new List<Point3D>();
            foreach (var face in ico.Faces)
            {
                double x = 0, y = 0, z = 0;
                foreach (int idx in face.VertexIndices)
                {
                    x += ico.Vertices[idx].X;
                    y += ico.Vertices[idx].Y;
                    z += ico.Vertices[idx].Z;
                }
                int n = face.VertexIndices.Count;
                faceCenters.Add(new Point3D(x / n, y / n, z / n));
            }

            double scale = 1.0 / faceCenters[0].Length();
            foreach (var c in faceCenters)
                dodeca.Vertices.Add(new Point3D(c.X * scale, c.Y * scale, c.Z * scale));

            for (int i = 0; i < ico.Vertices.Count; i++)
            {
                var ring = new List<int>();
                for (int f = 0; f < ico.Faces.Count; f++)
                    if (ico.Faces[f].VertexIndices.Contains(i))
                        ring.Add(f);

                if (ring.Count != 5) 
                    continue;

                var v = ico.Vertices[i];
                var first = faceCenters[ring[0]];
                var firstDir = first - v;
                var normal = new Point3D(v.X, v.Y, v.Z);
                var side = Point3D.Cross(normal, firstDir);
                side.Normalize();

                var ordered = ring.OrderBy(f => System.Math.Atan2(
                    Point3D.Dot(faceCenters[f] - v, side),
                    Point3D.Dot(faceCenters[f] - v, firstDir)
                )).ToList();

                dodeca.Faces.Add(new Polygon(ordered));
            }

            dodeca.BuildEdges();
            return dodeca;
        }

        public static Polyhedron CreateTetrahedron()
        {
            double a = 1.0 / System.Math.Sqrt(3.0);
            var p = new Polyhedron();
            p.Vertices.Add(new Point3D(a, a, a));
            p.Vertices.Add(new Point3D(a, -a, -a));
            p.Vertices.Add(new Point3D(-a, a, -a));
            p.Vertices.Add(new Point3D(-a, -a, a));

            p.Faces.Add(new Polygon(new List<int> { 0, 1, 2 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 3, 1 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 2, 3 }));
            p.Faces.Add(new Polygon(new List<int> { 1, 3, 2 }));

            p.BuildEdges();
            return p;
        }

        public static Polyhedron CreateCube()
        {
            var p = new Polyhedron();
            p.Vertices.Add(new Point3D(-1, -1, -1));
            p.Vertices.Add(new Point3D(1, -1, -1));
            p.Vertices.Add(new Point3D(1, 1, -1));
            p.Vertices.Add(new Point3D(-1, 1, -1));
            p.Vertices.Add(new Point3D(-1, -1, 1));
            p.Vertices.Add(new Point3D(1, -1, 1));
            p.Vertices.Add(new Point3D(1, 1, 1));
            p.Vertices.Add(new Point3D(-1, 1, 1));

            p.Faces.Add(new Polygon(new List<int> { 0, 1, 2, 3 }));
            p.Faces.Add(new Polygon(new List<int> { 4, 5, 6, 7 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 4, 7, 3 }));
            p.Faces.Add(new Polygon(new List<int> { 1, 5, 6, 2 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 1, 5, 4 }));
            p.Faces.Add(new Polygon(new List<int> { 3, 2, 6, 7 }));

            p.BuildEdges();
            return p;
        }

        public static Polyhedron CreateOctahedron()
        {
            var p = new Polyhedron();
            p.Vertices.Add(new Point3D(1, 0, 0));
            p.Vertices.Add(new Point3D(-1, 0, 0));
            p.Vertices.Add(new Point3D(0, 1, 0));
            p.Vertices.Add(new Point3D(0, -1, 0));
            p.Vertices.Add(new Point3D(0, 0, 1));
            p.Vertices.Add(new Point3D(0, 0, -1));

            p.Faces.Add(new Polygon(new List<int> { 0, 2, 4 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 4, 3 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 3, 5 }));
            p.Faces.Add(new Polygon(new List<int> { 0, 5, 2 }));
            p.Faces.Add(new Polygon(new List<int> { 1, 4, 2 }));
            p.Faces.Add(new Polygon(new List<int> { 1, 3, 4 }));
            p.Faces.Add(new Polygon(new List<int> { 1, 5, 3 }));
            p.Faces.Add(new Polygon(new List<int> { 1, 2, 5 }));

            p.BuildEdges();
            return p;
        }
    }
}