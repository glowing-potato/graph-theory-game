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
            public static string ToJSON(Graph graph)
            {
                Graph.Vertex[] vertices = graph.Vertices;
                List<KeyValuePair<int, int>> edges = graph.GetEdges();
                if (vertices != null && edges != null)
                {
                    string result = "{";
                    result += "\"v\":[";
                    double x = 0;
                    double y = 0;
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        x = ((int)(vertices[i].Y * 1000)) / 1000.0;
                        y = ((int)(vertices[i].X * 1000)) / 1000.0;
                        result += "[" + x + ",";
                        result += y + "]";
                        if (i != vertices.Length - 1) result += ",";
                    }
                    result += "]";
                    if(edges.Count != 0)
                    {
                        result += ",\"e\":[";
                        int e1 = 0;
                        int e2 = 0;
                        for (int i = 0; i < edges.Count; i++)
                        {
                            e1 = edges[i].Key;
                            e2 = edges[i].Value;
                            result += "[" + e1 + ",";
                            result += e2 + "]";
                            if (i != edges.Count - 1) result += ",";
                        }
                        result += "]}";
                        Console.WriteLine(result);
                        return result;
                    }
                    return "";
                }
                return "";
            }
        }

        // reflects upper triangle of the matrix to the lower triangle.
        private void reflectTopToBottom(ref bool[,] matrix, int order)
        {
            for (int i = 0; i < order; i++) for (int j = 0; j < order; j++)
                    if (j > i) matrix[j, i] = matrix[i, j];
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
        public void generateGraphsForLevels(int min, int max)
        {
            List<string> oilyGraphs = new List<string>();
            int oilyIndex = 0;
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
                            bool isOily = graph.HasEulerianPath();
                            //bool isHammy = graph.IsHamiltonian();
                            if (isOily)
                            {
                                string json = Convert.ToJSON(graph);
                                oilyGraphs.Add(json);
                                if(oilyGraphs.Count > 100)
                                {
                                    string[] lines = new string[oilyGraphs.Count + 2];
                                    lines[0] = "[";
                                    for(int i = 1; i < oilyGraphs.Count + 1; i++)
                                    {
                                        lines[i] = oilyGraphs[i - 1];
                                        lines[i] += (i < oilyGraphs.Count - 1) ? "," : "";
                                    }
                                    lines[lines.Length - 1] = "]";
                                    System.IO.File.WriteAllLines(@"C:/Users/Natha/Documents/Development"
                                        + "/HackKState/2018/graph-theory-game"
                                        + "/GraphGenerator/jsonEuler" + oilyIndex + ".json", lines);
                                    oilyGraphs = new List<string>();
                                    oilyIndex++;
                                }
                            }
                        }
                    }
                }
                sum += order;
                Console.WriteLine(order + "");
            }
        }

        // constructor
        public GenerateAdgacencyMatricies()
        {
            StreamWriter writer = new StreamWriter("D:/GraphTheoryGame/graph-theory-game/GraphGenerator/eulerian_trails.json");
            using (writer)
            {
                writer.Write("[\n  ");

                Random random = new Random();
                for (int i = 0; i < 50; i++)
                {
                    Graph graph = Graph.GenerateEulerianGraph(i / 5 + 5, random, (i + 3) / 5 + 3);

                    List<KeyValuePair<int, int>> edges = graph.GetEdges();
                    KeyValuePair<int, int> edge = edges[random.Next() % edges.Count];
                    //graph.RemoveEdge(edge.Key, edge.Value);
                    if (!graph.HasEulerianPath())
                    {
                        Console.WriteLine("Graph was not valid");
                        i--;
                        continue;
                    }
                    graph.SpreadVertices(200, random, 1.5, 0.01, true, 0.01);
                    writer.Write(Convert.ToJSON(graph) + (i < 49 ? ",\n  " : "\n"));
                }
                writer.Write("]");
            }

            StreamWriter writer2 = new StreamWriter("D:/GraphTheoryGame/graph-theory-game/GraphGenerator/hamiltonian_paths.json");
            using (writer2)
            {
                writer2.Write("[\n  ");

                Random random = new Random();
                for (int i = 0; i < 50; i++)
                {
                    Graph graph = Graph.GenerateHamiltonianGraph(i / 5 + 5, random, (i + 3) / 5);

                    List<KeyValuePair<int, int>> edges = graph.GetEdges();
                    KeyValuePair<int, int> edge = edges[random.Next() % edges.Count];
                    //graph.RemoveEdge(edge.Key, edge.Value);
                    /*if (!graph.IsHamiltonian())
                    {
                        Console.WriteLine("Graph was not valid");
                        i--;
                        continue;
                    }*/
                    graph.SpreadVertices(200, random, 1.5, 0.01, true, 0.01);
                    writer2.Write(Convert.ToJSON(graph) + (i < 49 ? ",\n  " : "\n"));
                }
                writer2.Write("]");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            GenerateAdgacencyMatricies gam = new GenerateAdgacencyMatricies();
            Console.ReadKey();
        }
    }
}
