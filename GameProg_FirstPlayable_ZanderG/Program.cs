using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProg_FirstPlayable_ZanderG
{
    internal class Program
    {
        static string map = "map.txt";
        
        static void Main(string[] args)
        {
            DisplayMap();
        }

        static void DisplayMap()
        {
            for (int i = 0; i < map.Length + 2; i++) Console.Write("░");

            Console.Write("\n");


        }
    }
}
