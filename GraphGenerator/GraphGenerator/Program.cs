using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphGenerator
{

    class GenerateAdgacencyMatricies
    {
        class Convert
        {
            // outputs JSON objects representing graphs
            //  origionally generated as matricies
            public static string ToJSON(Graph.Vertex[] vertecies, List<KeyValuePair<int, int>> edges)
            {
                string result = "";
                return result;
            }
        }

        // reflects upper triangle of the matrix to the lower triangle.
        private void reflectTopToBottom(ref bool[,] matrix, int order)
        {
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (j > i)
                    {
                        matrix[j, i] = matrix[i, j];
                    }
                }
            }
        }

        // generates the adjacency matrix for the specified order and id
        private bool[,] numToAdjacencyMatrix(int order, int id)
        {
            bool[,] result = new bool[order, order];
            for (int i = 0; i < order; i++)
            {
                bool[] row = new bool[order];
                for (int j = i + 1; j < order; j++)
                {
                    result[i, j] = (1 == (id % 2));
                    id /= 2;
                }
            }
            reflectTopToBottom(ref result, order);
            return result;
        }

        // counts the number of chs that show up in str
        private int count(string str, char ch)
        {
            int result = 0;
            foreach (char c in str) if (c == ch) result++;
            return result;
        }

        // generates all possible graphs as matricieswith a size anywhere between min and max verticies
        public void generateMatrixGraphsAsHTMLs(int min, int max)
        {
            int sum = 0;
            if(min != 1) for(int i = 0; i < min; i++) sum += i;
            for (int order = min; order <= max; order++)
            {
                for(int id = 0; id < Math.Pow(2, sum); id++)
                {
                    if (count(System.Convert.ToString(id, 2), '1') > order - 2)
                    {
                        bool[,] matrix = numToAdjacencyMatrix(order, id);
                        Graph graph = new Graph(order, matrix);
                        if(graph.IsConnected())
                        {
                            if (graph.IsEulerian())
                            {
                                string json = Convert.ToJSON(graph.Vertices, graph.GetEdges());
                                //TODO OUTPUT AND IMPLEMENT ToJSON
                            }
                        }
                    }
                }
            }
        }

        // constructor
        public GenerateAdgacencyMatricies()
        {
            generateMatrixGraphsAsHTMLs(1, 7);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GenerateAdgacencyMatricies gam = new GenerateAdgacencyMatricies();
        }
    }
}
