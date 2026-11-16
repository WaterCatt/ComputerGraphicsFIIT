using System.Collections.Generic;
using System.Drawing;

namespace Lab9
{
    public class Polygon
    {
        public List<int> VertexIndices;
        public List<int> NormalIndices;
        public List<int> TexCoordIndices;

        public Polygon(List<int> vertexIndices)
        {
            VertexIndices = vertexIndices;
            NormalIndices = new List<int>();
            TexCoordIndices = new List<int>();
        }

        public Polygon(List<int> vertexIndices, List<int> normalIndices, List<int> texCoordIndices)
        {
            VertexIndices = vertexIndices;
            NormalIndices = normalIndices ?? new List<int>();
            TexCoordIndices = texCoordIndices ?? new List<int>();
        }
    }
}