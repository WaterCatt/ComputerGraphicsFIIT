using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Lab7
{
    public static class ObjModelIO
    {
        public static Polyhedron Load(string path)
        {
            var polyhedron = new Polyhedron();
            var lines = File.ReadAllLines(path);
            var edgeList = new HashSet<Tuple<int, int>>();
            int vertexOffset = 0;

            foreach (var raw in lines)
            {
                var line = raw.Trim();
                if (line.Length == 0 || line[0] == '#')
                    continue;

                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0)
                    continue;

                switch (parts[0])
                {
                    case "v":
                        {
                            double x = ParseDouble(parts[1]);
                            double y = ParseDouble(parts[2]);
                            double z = ParseDouble(parts[3]);
                            polyhedron.Vertices.Add(new Point3D(x, y, z));
                            break;
                        }
                    case "f":
                        {
                            var indices = new List<int>();
                            for (int i = 1; i < parts.Length; i++)
                            {
                                var token = parts[i].Split('/')[0];
                                int idx = int.Parse(token, CultureInfo.InvariantCulture) - 1;
                                if (idx < 0)
                                    idx = polyhedron.Vertices.Count + idx;
                                indices.Add(idx);
                            }
                            if (indices.Count >= 3)
                                polyhedron.Faces.Add(new Polygon(indices));
                            break;
                        }
                    case "l":
                        {
                            var idxs = new List<int>();
                            for (int i = 1; i < parts.Length; i++)
                            {
                                int idx = int.Parse(parts[i], CultureInfo.InvariantCulture) - 1;
                                if (idx < 0)
                                    idx = polyhedron.Vertices.Count + idx;
                                idxs.Add(idx);
                            }
                            for (int i = 0; i + 1 < idxs.Count; i++)
                            {
                                int a = idxs[i], b = idxs[i + 1];
                                edgeList.Add(a < b ? Tuple.Create(a, b) : Tuple.Create(b, a));
                            }
                            break;
                        }
                }
                vertexOffset = 0;
            }

            polyhedron.BuildEdges();

            foreach (var e in edgeList)
                polyhedron.Edges.Add(e);

            return polyhedron;
        }

        public static void Save(Polyhedron polyhedron, string path)
        {
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine("# Lab7 OBJ export");

                foreach (var v in polyhedron.Vertices)
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "v {0} {1} {2}", v.X, v.Y, v.Z));

                if (polyhedron.Faces != null && polyhedron.Faces.Count > 0)
                {
                    foreach (var face in polyhedron.Faces)
                    {
                        if (face.VertexIndices.Count < 3)
                            continue;
                        writer.Write("f");
                        foreach (var idx in face.VertexIndices)
                            writer.Write(" " + (idx + 1).ToString(CultureInfo.InvariantCulture));
                        writer.WriteLine();
                    }
                }
                else
                {
                    var written = new HashSet<Tuple<int, int>>();
                    foreach (var edge in polyhedron.Edges)
                    {
                        var key = edge.Item1 < edge.Item2 ? edge : Tuple.Create(edge.Item2, edge.Item1);
                        if (!written.Add(key))
                            continue;
                        writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "l {0} {1}", key.Item1 + 1, key.Item2 + 1));
                    }
                }
            }
        }

        private static double ParseDouble(string s)
        {
            return double.Parse(s, CultureInfo.InvariantCulture);
        }
    }
}