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
            UseAsync();

        }

        static void UseAsync() {

            DoWork();
            Worker("main");
        }

        static async void DoWork() {
            await Worker("asnyc");
        }
        static void Worker(string name) {
            for (var x = 0; x < 5; x++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("{2} thread {0} print {1}", Thread.CurrentThread.ManagedThreadId, x, name);
            }
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
