using System;

namespace StringFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            Sprintf();
            Console.ReadKey();
        }
        static void Sprintf() {
            byte[] b = { 1, 2, 3 };
            string str = string.Format("{1}, {2}, {2}", b[0], b[1], b[2]);
            Console.WriteLine(str);
        }
    }
}
