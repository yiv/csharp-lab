using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintArray(new int[] { 1,3,5});
            PrintArray(new string[] {"6", "7", "8"});

            int[] ary = { 100, 200, 300 };
            PrintArray(ary);

            var strary = new[] { "abc", "ddd" };
            PrintArray(strary);

            Object[] objary = { 1, 2.2, "abc" };
            PrintArray(objary);


            Console.ReadLine();
        }
        static void PrintArray(int[] ary) {
            foreach (var x in ary) {
                Console.WriteLine(x);
            }

            Console.WriteLine();
        }

        static void PrintArray(string[] ary)
        {
            foreach (var x in ary)
            {
                Console.WriteLine(x);
            }

            Console.WriteLine();
        }

        static void PrintArray(Object[] ary)
        {
            foreach (var x in ary)
            {
                Console.WriteLine(x);
            }

            Console.WriteLine();
        }
    }
}
