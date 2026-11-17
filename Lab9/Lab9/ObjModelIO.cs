using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace Lab9
{
    public static class ObjModelIO
    {
        public static Polyhedron Load(string path)
        {
            var polyhedron = new Polyhedron();
            var lines = File.ReadAllLines(path);
            var edgeList = new HashSet<Tuple<int, int>>();

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
                    case "vn":
                        {
                            double x = ParseDouble(parts[1]);
                            double y = ParseDouble(parts[2]);
                            double z = ParseDouble(parts[3]);
                            polyhedron.ImportedNormals.Add(new Point3D(x, y, z));
                            break;
                        }
                    case "vt":
                        {
                            float u = (float)ParseDouble(parts[1]);
                            float v = parts.Length > 2 ? (float)ParseDouble(parts[2]) : 0f;
                            polyhedron.TexCoords.Add(new PointF(u, v));
                            break;
                        }
                    case "f":
                        {
                            var vIdx = new List<int>();
                            var tIdx = new List<int>();
                            var nIdx = new List<int>();
                            for (int i = 1; i < parts.Length; i++)
                            {
                                var token = parts[i].Split('/');
                                int vi = int.Parse(token[0], CultureInfo.InvariantCulture) - 1;
                                if (vi < 0) 
                                    vi = polyhedron.Vertices.Count + vi;
                                vIdx.Add(vi);

                                if (token.Length > 1 && token[1].Length > 0)
                                {
                                    int ti = int.Parse(token[1], CultureInfo.InvariantCulture) - 1;
                                    if (ti < 0) 
                                        ti = polyhedron.TexCoords.Count + ti;
                                    tIdx.Add(ti);
                                }
                                if (token.Length > 2 && token[2].Length > 0)
                                {
                                    int ni = int.Parse(token[2], CultureInfo.InvariantCulture) - 1;
                                    if (ni < 0) 
                                        ni = polyhedron.ImportedNormals.Count + ni;
                                    nIdx.Add(ni);
                                }
                                else
                                    nIdx.Add(-1);
                            }
                            if (vIdx.Count >= 3)
                                polyhedron.Faces.Add(new Polygon(vIdx, nIdx, tIdx));
                            break;
                        }
                    case "l":
                        {
                            var idxs = new List<int>();
                            for (int i = 1; i < parts.Length; i++)
                            {
                                int idx = int.Parse(parts[i], CultureInfo.InvariantCulture) - 1;
                                if (idx < 0) idx = polyhedron.Vertices.Count + idx;
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
            }

            polyhedron.BuildEdges();
            foreach (var e in edgeList)
                polyhedron.Edges.Add(e);

            polyhedron.ComputeFaceNormals();
            polyhedron.ComputeVertexNormals();

            return polyhedron;
        }

        public static void Save(Polyhedron polyhedron, string path)
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (var v in polyhedron.Vertices)
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "v {0} {1} {2}", v.X, v.Y, v.Z));

                bool useImportedNormals = polyhedron.ImportedNormals.Count > 0;
                if (useImportedNormals)
                {
                    foreach (var n in polyhedron.ImportedNormals)
                        writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "vn {0} {1} {2}", n.X, n.Y, n.Z));
                }

                if (polyhedron.TexCoords.Count > 0)
                {
                    foreach (var t in polyhedron.TexCoords)
                        writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "vt {0} {1}", t.X, t.Y));
                }

                bool hasNormals = useImportedNormals;
                bool hasTex = polyhedron.TexCoords.Count > 0;

                if (polyhedron.Faces != null && polyhedron.Faces.Count > 0)
                {
                    foreach (var face in polyhedron.Faces)
                    {
                        if (face.VertexIndices.Count < 3)
                            continue;
                        writer.Write("f");
                        for (int i = 0; i < face.VertexIndices.Count; i++)
                        {
                            int vi = face.VertexIndices[i] + 1;
                            string token = vi.ToString(CultureInfo.InvariantCulture);
                            if (hasTex && i < face.TexCoordIndices.Count)
                                token += "/" + (face.TexCoordIndices[i] + 1).ToString(CultureInfo.InvariantCulture);
                            else if (hasNormals)
                                token += "/";
                            if (hasNormals && i < face.NormalIndices.Count && face.NormalIndices[i] >= 0 && face.NormalIndices[i] < polyhedron.ImportedNormals.Count)
                                token += "/" + (face.NormalIndices[i] + 1).ToString(CultureInfo.InvariantCulture);
                            writer.Write(" " + token);
                        }
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
