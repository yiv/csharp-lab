using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(()=> {
                for (var x = 0; x < 5; x++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{2} on thread {0} print {1}", Thread.CurrentThread.ManagedThreadId, x, "task");
                }
            });
            Worker("main");

        }

        static void Worker(string name)
        {
            for (var x = 0; x < 5; x++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("{2} on thread {0} print {1}", Thread.CurrentThread.ManagedThreadId, x, name);
            }
        }
    }
}
