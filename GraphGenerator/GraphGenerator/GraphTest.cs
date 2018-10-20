using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerator
{
    public class GraphTest
    {

        public static void Main(string[] args)
        {

            bool[,] matrix1 = new bool[,] { { false, true, true }, { true, false, true }, { true, true, false } };
            bool[,] matrix2 = new bool[,] { { false, true, true, false }, { true, false, true, true }, { true, true, false, true}, { false, true, true, false } };
            bool[,] matrix3 = new bool[,] { { false, true, false, false }, { true, false, false, false }, { false, false, false, true }, { false, false, true, false } };
            List<KeyValuePair<int, int>> edges1 = new List<KeyValuePair<int, int>>();
            edges1.Add(new KeyValuePair<int, int>(0, 1));
            edges1.Add(new KeyValuePair<int, int>(0, 2));
            edges1.Add(new KeyValuePair<int, int>(1, 2));
            Graph g1 = new Graph(3, matrix1);
            Graph g2 = new Graph(4, matrix2);
            Graph g3 = new Graph(4, matrix3);
            Assert(3, g1.Order);
            Assert(3, g1.Vertices.Length);
            Assert(true, g1.IsConnected());
            Assert(true, g1.IsEulerian());
            Assert(true, g1.HasEulerianPath());
            //Assert(edges1, g1.GetEdges());
            Assert(4, g2.Order);
            Assert(4, g2.Vertices.Length);
            Assert(true, g2.IsConnected());
            Assert(false, g2.IsEulerian());
            Assert(true, g2.HasEulerianPath());
            Assert(4, g3.Order);
            Assert(4, g3.Vertices.Length);
            Assert(false, g3.IsConnected());
            Assert(false, g3.IsEulerian());
            Assert(false, g3.HasEulerianPath());
            Console.ReadKey();

        }

        private static void Assert(object expected, object actual)
        {
            if (!expected.Equals(actual))
            {
                throw new InvalidOperationException("Assertion was " + expected.ToString() + " but needs to be " + actual.ToString());
            }
        }

    }
}
