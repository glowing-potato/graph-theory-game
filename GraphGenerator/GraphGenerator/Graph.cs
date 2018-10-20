using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerator
{
    public class Graph
    {
        private class Vertex
        {
            private double x;
            private double y;
        }

        private int order;
        private Vertex[] vertices;
        private bool[][] adjMatrix;

        public Graph(int order, bool[][] adjMatrix)
        {
            this.order = order;
            this.adjMatrix = adjMatrix;
        }

        public void AddEdge(int vertex1, int vertex2)
        {

        }

        public void RemoveEdge(int vertex1, int vertex2)
        {

        }

        public void AddVertex()
        {

        }

        public void RemoveVertex(int position)
        {

        }

        public bool IsHamiltonian()
        {
            return false;
        }

        public bool IsEulerian()
        {
            return false;
        }

        public bool IsIsomorphicTo(Graph g)
        {
            return false;
        }

        public int Order { get => order; }

    }
}
