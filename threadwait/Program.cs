using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threadwait
{
    class Program
    {
        static private AutoResetEvent wg;
        static void Main(string[] args)
        {
            wg = new AutoResetEvent(false);

            var t1 = new Thread(Worker);
            t1.IsBackground = true;
            t1.Start();


            var t2 = new Thread(Worker1);
            t2.IsBackground = true;
            t2.Start();

            wg.WaitOne();
            wg.WaitOne();

        }

        static void Worker() {
            for (var x = 0; x < 5; x++) {
                Thread.Sleep(500);
                Console.WriteLine("Worker on thread {0}, {1} ", Thread.CurrentThread.ManagedThreadId, x);
            }
            wg.Set();
        }

        static void Worker1()
        {
            for (var x = 0; x < 5; x++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Worker1 on thread {0}, {1} ", Thread.CurrentThread.ManagedThreadId, x);
            }
            wg.Set();
        }
    }
}
