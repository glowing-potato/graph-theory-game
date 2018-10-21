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

            public Vertex(double x, double y, Graph parent)
            {
                X = x;
                Y = y;
                Parent = parent;
            }

            public double X { get; set; }

            public double Y { get; set; }

            private Graph Parent { get; set; }

            public int Degree
            {
                get
                {
                    int degree = 0;
                    for (int i = 0; i < Parent.Order; i++) {
                        degree += Parent.AdjacencyMatrix[Parent.IndexOfVertex(this), i] ? 1 : 0;
                    }
                    return degree;
                }
            }
        }

        public Graph(int order)
        {
            Order = order;
            AdjacencyMatrix = new bool[Order, Order];
            Vertices = new Vertex[Order];
            for (int i = 0; i < Order; i++)
            {
                Vertices[i] = new Vertex(0.5, 0.5, this);
            }
        }

        public Graph(int order, bool[,] adjMatrix)
        {
            Order = order;
            AdjacencyMatrix = adjMatrix;
            Vertices = new Vertex[Order];
            for (int i = 0; i < Order; i++)
            {
                Vertices[i] = new Vertex(0.5, 0.5, this);
            }
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            AdjacencyMatrix[vertex1,vertex2] = true;
            AdjacencyMatrix[vertex2,vertex1] = true;
        }

        public void RemoveEdge(int vertex1, int vertex2)
        {
            AdjacencyMatrix[vertex1,vertex2] = false;
            AdjacencyMatrix[vertex2,vertex1] = false;
        }

        public List<KeyValuePair<int, int>> GetEdges()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < Order; i++)
            {
                for (int j = i + 1; j < Order; j++)
                {
                    if (AdjacencyMatrix[i,j])
                    {
                        list.Add(new KeyValuePair<int, int>(i, j));
                    }
                }
            }
            return list;
        }

        public Vertex GetVertex(int vertex)
        {
            return Vertices[vertex];
        }

        public int IndexOfVertex(Vertex vertex)
        {
            for (int i = 0; i < Order; i++)
            {
                if (vertex == Vertices[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void AddVertex(double x, double y)
        {
            Vertex[] temp = new Vertex[Order + 1];
            Vertices.CopyTo(temp, 0);
            temp[Order] = new Vertex(x, y, this);
            Vertices = temp;
            bool[,] adjMatrix = new bool[Order + 1, Order + 1];
            for (int i = 0; i < Order; i++)
            {
                for (int j = 0; j < Order; j++)
                {
                    adjMatrix[i, j] = AdjacencyMatrix[i, j];
                }
            }
            AdjacencyMatrix = adjMatrix;
            Order++;
        }

        public void RemoveVertex(int position)
        {
            Vertex[] temp = new Vertex[Order - 1];
            int j = 0;
            for (int i = 0; i < Order; i++)
            {
                if (i == position)
                {
                    continue;
                }
                temp[j] = Vertices[i];
                j++;
            }
            Order--;
            Vertices = temp;
            /*bool[,] adjMatrix = new bool[Order - 1, Order - 1];
            int k = 0;
            int l = 0;
            for (int i = 0; i < Order; i++)
            {
                if (i == position)
                {
                    continue;
                }
                for (int j = 0; j < Order; j++)
                {
                    if (j == position)
                    {
                        continue;
                    }
                    temp[j] = Vertices[i];
                    j++;
                }
                i++;
            }*/
        }

        public void MakePlanar()
        {

            for (int i = 0; i < Order; i++)
            {
                Vertex vertex1 = Vertices[i];
                for (int j = 0; j < Order; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    Vertex vertex2 = Vertices[j];
                    for (int k = 0; k < Order; k++)
                    {
                        Vertex vertex3 = Vertices[k];
                        for (int l = 0; l < Order; l++)
                        {
                            if (k == l)
                            {
                                continue;
                            }
                            Vertex vertex4 = Vertices[l];
                            double a = (vertex2.Y - vertex1.Y) / (double) (vertex2.X - vertex1.X);
                            double b = vertex1.Y - a * vertex1.X;
                            double c = (vertex4.Y - vertex3.Y) / (double)(vertex4.X - vertex3.X);
                            double d = vertex3.Y - c * vertex3.X;
                            double x = (d - b) / (a - c);
                            if (x >= Math.Min(Math.Min(vertex1.X, vertex2.X), Math.Min(vertex3.X, vertex4.X)) && x <= Math.Max(Math.Max(vertex1.X, vertex2.X), Math.Max(vertex3.X, vertex4.X)))
                            {
                                AdjacencyMatrix[i,j] = false;
                                AdjacencyMatrix[j,i] = false;
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
                    for (int i = 0; i < Order; i++)
                    {
                        if (AdjacencyMatrix[cur, i])
                        {
                            stack.Push(i);
                        }
                    }
                }
                
            }
            return usedVertices.Count == Order;
        }

        public bool IsHamiltonian()
        {
            return false;
        }

        public bool IsEulerian()
        {
            for (int i = 0; i < Order; i++)
            {
                int degree = 0;
                for (int j = 0; j < Order; j++)
                {
                    degree += AdjacencyMatrix[i,j] ? 1 : 0;
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
            for (int i = 0; i < Order; i++)
            {
                int degree = 0;
                for (int j = 0; j < Order; j++)
                {
                    degree += AdjacencyMatrix[i, j] ? 1 : 0;
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

        public void MergeGraphs(Graph g, int thisVertex, int gVertex)
        {
            int oldOrder = Order;
            foreach (Vertex v in g.Vertices)
            {
                AddVertex(v.X, v.Y);
            }
            foreach (KeyValuePair<int, int> edge in g.GetEdges())
            {
                AddEdge(oldOrder + edge.Key, oldOrder + edge.Value);
            }
            AddEdge(thisVertex, oldOrder + gVertex);
        }

        public void SpreadVertices(int iterations, Random random, double targetEdgeLength, double multiplier, bool sigmoid, double jitter)
        {
            for (int itr = 0; itr < iterations; itr++)
            {
                for (int i = 0; i < Order; i++)
                {
                    for (int j = 0; j < Order; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }
                        double length = Math.Sqrt(Math.Pow(Vertices[j].X - Vertices[i].X, 2) + Math.Pow(Vertices[j].Y - Vertices[i].Y, 2));
                        double force = ((length / targetEdgeLength) - 1) * multiplier;
                        double angle = Math.Atan2(Vertices[j].Y - Vertices[i].Y, Vertices[j].X - Vertices[i].X);
                        Vertices[i].X += Math.Cos(angle) * force + random.NextDouble() * jitter;
                        Vertices[i].Y += Math.Sin(angle) * force + random.NextDouble() * jitter;
                        Vertices[j].X -= Math.Cos(angle) * force + random.NextDouble() * jitter;
                        Vertices[j].Y -= Math.Sin(angle) * force + random.NextDouble() * jitter;
                    }
                }
            }
            for (int i = 0; sigmoid && i < Order; i++)
            {
                Vertices[i].X = (Math.Tanh(Vertices[i].X - 0.5) + 1) / 2.0;
                Vertices[i].Y = (Math.Tanh(Vertices[i].Y - 0.5) + 1) / 2.0;
            }
        }

        public static Graph GenerateEmptyGraph(int order)
        {
            return new Graph(order);
        }

        public static Graph GenerateCycleGraph(int order)
        {
            Graph g = new Graph(order);
            for (int i = 0; i < order; i++)
            {
                g.AddEdge(i, (i + 1) % order);
            }
            return g;
        }

        public static Graph GenerateCompleteGraph(int order)
        {
            Graph g = new Graph(order);
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (i != j)
                    {
                        g.AdjacencyMatrix[i, j] = true;
                    }
                }
            }
            return g;
        }

        public static Graph GenerateHamiltonianCycleGraph(int order, Random random, int minExtra)
        {
            Graph g = GenerateCycleGraph(order);
            for (int i = 0; i < Math.Max(minExtra, random.Next() % order); i++)
            {
                int a = 0, b = 0;
                while (a == b)
                {
                    a = random.Next() % order;
                    b = random.Next() % order;
                }
                g.AddEdge(a, b);

            }
            return g;
        }

        public static Graph GenerateHamiltonianPathGraph(int order, Random random, int minExtra)
        {
            Graph g = new Graph(order);
            for (int i = 0; i < order; i++)
            {
                g.AddEdge(i, (i + 1) % order);
            }
            for (int i = 0; i < Math.Max(minExtra, random.Next() % order); i++)
            {
                int a = 0, b = 0;
                while (a == b)
                {
                    a = random.Next() % order;
                    b = random.Next() % order;
                }
                g.AddEdge(a, b);
                
            }
            return g;
        }

        public static Graph GenerateEulerianTrailGraph(int order, Random random, int maxcycle)
        {
            List<Graph> cycles = new List<Graph>();
            int total = 0;
            while (total < order)
            {
                int cycle = Math.Min((random.Next() % (maxcycle - 1)) + 2, order - total);
                total += cycle;
                cycles.Add(GenerateCycleGraph(cycle));
            }
            Graph g = cycles[0];
            for (int i = 1; i < cycles.Count; i++)
            {
                g.MergeGraphs(cycles[i], random.Next() % g.Order, random.Next() % cycles[i].Order);
            }
            return g;
        }

        public static Graph GenerateEulerianCircuitGraph(int order, Random random, int maxcycle)
        {
            Graph g = GenerateEulerianTrailGraph(order, random, maxcycle);
            Vertex[] oddDegrees = null;
            while (oddDegrees == null || oddDegrees.Count() > 0)
            {
                oddDegrees = g.Vertices.Where((z) => z.Degree % 2 == 1).ToArray();
                for (int i = 0; i < oddDegrees.Length; i += 2)
                {
                    g.AddEdge(g.IndexOfVertex(oddDegrees[i]), g.IndexOfVertex(oddDegrees[i + 1]));
                }
            }
            return g;
        }

        public int Degree {
            get {
                int degree = 0;
                for (int i = 0; i < Order; i++)
                {
                    degree += GetVertex(i).Degree;
                }
                return degree / 2;
            }
        }

        public int MaxDegree
        {
            get
            {
                int maxDegree = 0;
                for (int i = 0; i < Order; i++)
                {
                    if (Vertices[i].Degree > maxDegree)
                    {
                        maxDegree = Vertices[i].Degree;
                    }
                }
                return maxDegree;
            }
        }

        public int MaxDegreeIndex
        {
            get
            {
                int maxDegree = 0;
                int maxIndex = 0;
                for (int i = 0; i < Order; i++)
                {
                    if (Vertices[i].Degree > maxDegree)
                    {
                        maxDegree = Vertices[i].Degree;
                        maxIndex = i;
                    }
                }
                return maxIndex;
            }
        }

        public int Order { get; private set; }

        public Vertex[] Vertices { get; private set; }
        
        public bool[,] AdjacencyMatrix { get; private set; }

    }
}
