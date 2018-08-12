using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main start");
            Task<string> t = GetData();
            Console.WriteLine("Main after async call");
            t.Wait();
            string result = t.Result;
            Console.WriteLine("Main end; result: " + result);
            Console.ReadKey();
        }
        static async Task<string> GetData()
        {
            Console.WriteLine("Before long operation");

            await Task.Run(() =>
            {
                Console.WriteLine("Task before");
                Thread.Sleep(3000);
                Console.WriteLine("Task after");
            });
            Thread.Sleep(3000);

            Console.WriteLine("After long operation");

            return "Hello world";
        }
    }
}
