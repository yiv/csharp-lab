using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lambda
{
    class Program
    {
        delegate int del(int i);
        delegate void print(string i);
        static void Main(string[] args)
        {
            Test3();
            Console.ReadKey();
        }
        public static void Test1() { 
            del myfun = x => x * x;
            var j = myfun(5);
            Console.WriteLine(j);
        }
        public static void Test2()
        {
            print myfun;
            myfun = x => {
                string s = x + " World";
                Console.WriteLine(s);
            };
            myfun("heheh");
        }
        public static void Test3()
        {
            Action<int, string> too = (int x, string s) => {
                Console.WriteLine(x);
                Console.WriteLine(s);
            };
            too(500, "hek");
        }
    }
}
