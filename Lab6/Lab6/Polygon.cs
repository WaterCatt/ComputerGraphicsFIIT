using System.Collections.Generic;

namespace Lab6
{
    public class Polygon
    {
        public List<int> VertexIndices;

        public Polygon(List<int> vertexIndices)
        {
            VertexIndices = vertexIndices;
        }

        public Polygon Copy()
        {
            return new Polygon(new List<int>(VertexIndices));
        }
    }
}