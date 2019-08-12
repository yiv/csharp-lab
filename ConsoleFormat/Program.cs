using System;

namespace strings
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = "this is a string  ";
            var y = "  this is another string";

            Console.WriteLine(x.Length);
            Console.WriteLine(x.CompareTo(y));
            Console.WriteLine(x.Contains("str") == true);
            Console.WriteLine(x.Insert(x.IndexOf("a") +1 , " good"));
            Console.WriteLine(x.Replace("string", "str"));
            Console.WriteLine(x.Split(" ")[0]);
            Console.WriteLine(y.Trim()); 
            Console.WriteLine(x + y);



            Console.ReadKey();
        }
        static void Sprintf() {
            byte[] b = { 1, 2, 3 };
            string str = string.Format("{1}, {2}, {2}", b[0], b[1], b[2]);
            Console.WriteLine(str);
        }
    }
}
