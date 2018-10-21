using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GenerateGraphs("D:/GraphTheoryGame/graph-theory-game/src/games/EulerTrail.json", (random, i) =>
            {
                Graph g = null;
                while (g == null || !g.HasEulerianPath())
                {
                    g = Graph.GenerateEulerianTrailGraph(i / 5 + 5, random, (i + 3) / 5 + 3);
                }
                return g;
            });
            GenerateGraphs("D:/GraphTheoryGame/graph-theory-game/src/games/EulerCycle.json", (random, i) => 
            {
                Graph g = null;
                while (g == null || !g.IsEulerian())
                {
                    g = Graph.GenerateEulerianCircuitGraph(i / 5 + 5, random, (i + 3) / 5 + 3);
                }
                return g;
            });
            GenerateGraphs("D:/GraphTheoryGame/graph-theory-game/src/games/HamiltonPath.json", (random, i) => Graph.GenerateHamiltonianPathGraph(i / 5 + 5, random, (i + 3) / 5));
            GenerateGraphs("D:/GraphTheoryGame/graph-theory-game/src/games/HamiltonCircuit.json", (random, i) => Graph.GenerateHamiltonianCycleGraph(i / 5 + 5, random, (i + 3) / 5));
        }

        public static void GenerateGraphs(string path, Func<Random, int, Graph> genFunc)
        {
            StreamWriter writer = new StreamWriter(path);
            using (writer)
            {
                writer.Write("[\n  ");

                Random random = new Random();
                for (int i = 0; i < 50; i++)
                {
                    Graph graph = genFunc(random, i);
                    graph.SpreadVertices(200, random, 1.5, 0.01, true, 0.01);
                    writer.Write(GraphToJSON(graph) + (i < 49 ? ",\n  " : "\n"));
                }
                writer.Write("]");
            }
        }

        public static string GraphToJSON(Graph graph)
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
                if (edges.Count != 0)
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
}
