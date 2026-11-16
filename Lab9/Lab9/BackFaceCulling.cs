using System;
using System.Collections.Generic;

namespace Lab9
{
    public static class BackFaceCulling
    {
        public static HashSet<Tuple<int, int>> GetVisibleEdges(Polyhedron polyhedron, Camera camera)
        {
            return Culling.GetVisibleEdges(polyhedron, camera);
        }
    }
}