using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerator
{
    public class Graph
    {
        public class Vertex
        {
            private double x;
            private double y;

            public Vertex(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public double X { get => x; set => x = value; }
            public double Y { get => y; set => y = value; }
        }

        private int order;
        private Vertex[] vertices;
        private bool[,] adjMatrix;

        public Graph(int order, bool[,] adjMatrix)
        {
            this.order = order;
            this.adjMatrix = adjMatrix;
            this.vertices = new Vertex[order];
            Random random = new Random();
            for (int i = 0; i < order; i++)
            {
                vertices[i] = new Vertex(random.NextDouble(), random.NextDouble());
            }
        }

        public Graph(int order, Vertex[] vertices, bool[,] adjMatrix)
        {
            this.order = order;
            this.vertices = vertices;
            this.adjMatrix = adjMatrix;
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            adjMatrix[vertex1,vertex2] = true;
            adjMatrix[vertex2,vertex1] = true;
        }

        public void RemoveEdge(int vertex1, int vertex2)
        {
            adjMatrix[vertex1,vertex2] = false;
            adjMatrix[vertex2,vertex1] = false;
        }

        public List<KeyValuePair<int, int>> GetEdges()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < order; i++)
            {
                for (int j = i + 1; j < order; j++)
                {
                    if (adjMatrix[i,j])
                    {
                        list.Add(new KeyValuePair<int, int>(i, j));
                    }
                }
            }
            return list;
        }

        public void AddVertex(double x, double y)
        {
            Vertex[] temp = new Vertex[order + 1];
            vertices.CopyTo(temp, 0);
            vertices[order] = new Vertex(x, y);
            vertices = temp;
            order++;
        }

        public void RemoveVertex(int position)
        {
            Vertex[] temp = new Vertex[order - 1];
            int j = 0;
            for (int i = 0; i < order; i++)
            {
                if (i == position)
                {
                    continue;
                }
                temp[j] = vertices[i];
                j++;
            }
            order--;
            vertices = temp;
        }

        public void MakePlanar()
        {

            for (int i = 0; i < order; i++)
            {
                Vertex vertex1 = vertices[i];
                for (int j = 0; j < order; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    Vertex vertex2 = vertices[j];
                    for (int k = 0; k < order; k++)
                    {
                        Vertex vertex3 = vertices[k];
                        for (int l = 0; l < order; l++)
                        {
                            if (k == l)
                            {
                                continue;
                            }
                            Vertex vertex4 = vertices[l];
                            double a = (vertex2.Y - vertex1.Y) / (double) (vertex2.X - vertex1.X);
                            double b = vertex1.Y - a * vertex1.X;
                            double c = (vertex4.Y - vertex3.Y) / (double)(vertex4.X - vertex3.X);
                            double d = vertex3.Y - c * vertex3.X;
                            double x = (d - b) / (a - c);
                            if (x >= Math.Min(Math.Min(vertex1.X, vertex2.X), Math.Min(vertex3.X, vertex4.X)) && x <= Math.Max(Math.Max(vertex1.X, vertex2.X), Math.Max(vertex3.X, vertex4.X)))
                            {
                                adjMatrix[i,j] = false;
                                adjMatrix[j,i] = false;
                            }
                        }
                    }
                }
            }
            
        }

        public bool IsConnected()
        {
            List<int> usedVertices = new List<int>();
            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            while (stack.Count > 0)
            {
                int cur = stack.Pop();
                if (!usedVertices.Contains(cur))
                {
                    usedVertices.Add(cur);
                    for (int i = 0; i < order; i++)
                    {
                        if (adjMatrix[cur, i])
                        {
                            stack.Push(i);
                        }
                    }
                }
                
            }
            return usedVertices.Count == order;
        }

        public bool IsHamiltonian()
        {
            return false;
        }

        public bool IsEulerian()
        {
            for (int i = 0; i < order; i++)
            {
                int degree = 0;
                for (int j = 0; j < order; j++)
                {
                    degree += adjMatrix[i,j] ? 1 : 0;
                }
                if (degree % 2 == 1)
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasEulerianPath()
        {
            int oddDegrees = 0;
            for (int i = 0; i < order; i++)
            {
                int degree = 0;
                for (int j = 0; j < order; j++)
                {
                    degree += adjMatrix[i, j] ? 1 : 0;
                }
                if (degree % 2 == 1)
                {
                    oddDegrees++;
                    if (oddDegrees > 2) { return false; }
                }
            }
            return true;
        }
            
        public bool IsIsomorphicTo(Graph g)
        {
            return false;
        }

        public int Order { get => order; }

        public Vertex[] Vertices { get => vertices; }
        
        public bool[,] AdjacencyMatrix { get => adjMatrix; }

    }
}
