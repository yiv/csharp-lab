using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threadpool
{
    class Program
    {
        static void Main(string[] args)
        {

            WaitCallback waitCallback = new WaitCallback(Printer);

            for (var x = 0; x < 100; x++) {
                ThreadPool.QueueUserWorkItem(waitCallback);
            }

            Console.ReadLine();
        }


        static void Printer(Object obj) {
            for (var x = 0; x < 5; x++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Worker1 on thread {0}, {1} ", Thread.CurrentThread.ManagedThreadId, x);
            }
        }
    }
}
