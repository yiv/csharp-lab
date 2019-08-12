using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datatype
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = "Hello";
            Console.WriteLine(x);
            Console.WriteLine(x.GetType());
            Console.WriteLine(x.ToLower());
            Console.WriteLine(x.GetHashCode());

            Object y = 999;
            Console.WriteLine(y.ToString());
            Console.WriteLine(y.GetHashCode());


            Console.WriteLine(int.MaxValue);
            Console.WriteLine(double.Epsilon);
            Console.WriteLine(double.Parse("6.6")-1);


            var n = 500000;
            Console.WriteLine(((byte)n));
            Console.WriteLine(byte.MaxValue);

            Console.WriteLine(char.MaxValue);





            Console.ReadLine();
        }
    }
}
