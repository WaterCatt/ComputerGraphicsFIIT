using System.Collections.Generic;

namespace Lab6
{
    public class Polygon
    {
        public List<int> VertexIndices { get; set; }

        public List<Point3D> UVs { get; set; }
        public List<Point3D> Normals { get; set; }

        public Polygon(List<int> vertexIndices)
        {
            VertexIndices = vertexIndices;
            UVs = null;
            Normals = null;
        }

        public Polygon Copy()
        {
            return new Polygon(new List<int>(VertexIndices));
        }
    }
}