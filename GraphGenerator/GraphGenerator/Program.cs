using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphGenerator
{
    // a matrix row
    internal class Row
    {
        public List<int> items { get; } = new List<int>();
        public Row(List<int> _items)
        {
            items = _items;
        }
        public void addItem(int item)
        {
            items.Add(item);
        }
    }

    // a matrix
    internal class Matrix
    {
        public List<Row> rows { get; } = new List<Row>();
        public void addRow(Row row)
        {
            rows.Add(row);
        }
        public void reflectTopToBottom()
        {
            if(rows.Count == rows[0].items.Count)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    for (int j = 0; j < rows[i].items.Count; j++)
                    {
                        if (j > i)
                        {
                            rows[j].items[i] = rows[i].items[j];
                        }
                    }
                }
            }
        }

        // generates a list of strings or this matrix
        public List<string> save()
        {
            List<string> lines = new List<string>();
            for(int i = 0; i < rows.Count; i++)
            {
                lines.Add("");
                for(int j = 0; j < rows[i].items.Count; j++)
                {
                    lines[i] += rows[i].items[j] + " ";
                }
            }
            return lines;
        }
    }

    class GenerateAdgacencyMatricies
    {
        class Convert
        {
            // outputs JSON objects representing graphs
            //  origionally generated as matricies
            public static void ToJSON()
            {

            }
            // outputs Zach's preferred output of lists
            //  of verticies [(x, y)] and edges [(v, v)]
            public static void ToVertEdge()
            {

            }
        }

        // takes in a list of lists of matrix rows and saves it to an html file
        private void saveToHTML(List<List<string>> files, int size)
        {
            int totalLines = 0;
            foreach (List<string> file in files) totalLines += file.Count;
            string[] lines = new string[totalLines + 1];
            int i = 0;
            lines[0] = "<code>";
            foreach (List<string> file in files)
            {
                lines[i] += "<br/><br/>";
                foreach (string line in file)
                {
                    lines[i] += "<br/>";
                    lines[i] += line;
                    i++;
                }
            }
            lines[i] = "</code>";
            System.IO.File.WriteAllLines(@"C:/Users/Natha/Documents/Development"
                        + "/HackKState/2018/graph-theory-game"
                        + "/GraphGenerator/" + size + ".html", lines);
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
            for (int size = min; size <= max; size++)
            {
                List<List<string>> files = new List<List<string>>();
                for (int num = 0; num < Math.Pow(2, sum); num++)
                {
                    int bin = num;
                    if (count(System.Convert.ToString(bin, 2), '1') > sum - size)
                    {
                        Matrix matrix = new Matrix();
                        for (int i = 0; i < size; i++)
                        {
                            List<int> items = new List<int>();
                            for (int j = 0; j < size; j++)
                            {
                                if (j > i)
                                {
                                    items.Add(bin % 2);
                                    bin /= 2;
                                }
                                else items.Add(0);
                            }
                            Row row = new Row(items);
                            matrix.addRow(row);
                        }
                        matrix.reflectTopToBottom();
                        files.Add(matrix.save());
                    }
                }
                sum += size;
                saveToHTML(files, size);
                Console.WriteLine("FINISHED " + size);
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
