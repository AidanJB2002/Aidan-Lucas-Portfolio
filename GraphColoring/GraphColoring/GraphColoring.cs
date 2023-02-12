using System;
using System.IO;
#pragma warning disable

namespace GraphColoring
{	
	class MColor
	{
		public const int INFINITY = 30000;
        public static int[][] adjacency;         // The adjacency matrix
        public static int numVertex;             // Number of vertices        
		private static int numsol;		
		private static int [] vcolor;
		private static int numcolors;
		private static StreamWriter wtr;
		private static StreamReader rdr;
				

        public static void createGraph(StreamReader rdr)
        {
			string line;
            string[] s; // for split         
            int i, j;
			try
			{
                line = rdr.ReadLine(); // comment line
                line = rdr.ReadLine(); // reads # rows as a string
                s = line.Split(null); // pass null to use whitespace. n is in s[0]
                numVertex = Convert.ToInt32(s[0]);
                vcolor = new int[numVertex];
                wtr.WriteLine("Number of rows: " + numVertex);
                wtr.WriteLine();

                // Dynamically allocate matrix

                adjacency = new int[numVertex][];
                for (i = 0; i < numVertex; i++)
                {
                    adjacency[i] = new int[numVertex];
                    for (j = 0; j < numVertex; j++)
                    {
                        adjacency[i][j] = 0;
                    }
                }
                line = rdr.ReadLine(); // second comment line
                line = rdr.ReadLine(); // get line of numbers in data file
                s = line.Split(null);
                while (Convert.ToInt32(s[0]) != -1)
                {
                    adjacency[Convert.ToInt32(s[0])][Convert.ToInt32(s[1])] = 1;
                    adjacency[Convert.ToInt32(s[1])][Convert.ToInt32(s[0])] = 1;
                    line = rdr.ReadLine(); // get next row
                    s = line.Split(null);
                }
            } // end try
            catch (IOException e)
            {
                Console.WriteLine("Some I/O problem", e.ToString());
            }
        } // end CreateGraph	

        public static void printGraph()
        {
            int i, j;
            wtr.WriteLine("Our matrix is:\n");
            for (i = 0; i < numVertex; i++)
            {
                for (j = 0; j < numVertex; j++)
                    wtr.Write("{0,8:D}", adjacency[i][j]);
                wtr.WriteLine();
            }
            wtr.WriteLine();
            wtr.WriteLine("Colors are:\n");
            rdr.Close();
            return;
        }
        public static void m_coloring(int i)
        {
            int color;
            for (color = 0; color < numcolors; color++)
            {
                vcolor[i + 1] = color; //set the color
                if (promising(i + 1)) //check if the color is promising
                {
                    if (i + 1 == numVertex - 1) //check if the last vertex has been assigned a color
                    {
                        for(int j = 0; j < vcolor.Length; j++)
                        {
                            wtr.Write(vcolor[j] + " ");
                        }
                        wtr.WriteLine();
                        numsol++; //add a solution to the counter
                    }
                    else
                    {
                        m_coloring(i + 1); //assign the next color
                    }
                }
            }
        }
        public static bool promising(int i)
        {
            int k;
            bool Switch;
            k = 0;
            Switch = true;
            while (k < i && Switch) //vertex i and k cannot be the same color
            {
                if (adjacency[i][k] == 1 && vcolor[i] == vcolor[k]) //check if promising
                {
                    Switch = false;
                }
                k++;
            }
            return Switch; //return true
        }
		static void Main(string[] args)
		{	
            String filename1 = "results.txt";   // output file
            String filename2 = "ColorA.txt";    // input file
            
            try
			{			
			     wtr = new StreamWriter(filename1);
			     rdr = new StreamReader(filename2);
			
			     createGraph(rdr);
				 
                 printGraph();
			     
			     numcolors = 4;
			     numsol = 0;
				 
			     m_coloring(-1);  // 0 is start vertex
                 wtr.WriteLine();

                 wtr.WriteLine("Number of solutions: " + numsol);
			     wtr.Close();
			}
			catch (FileNotFoundException e)
			{
				 Console.WriteLine("One of your files was not found.");
            }
		}
	}
}