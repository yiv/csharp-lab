using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace asyncDelegate
{
    public delegate int BinaryOp(int x, int y);
    class Program
    {
        private static bool isDone = false;
        static void Main(string[] args)
        {
            Test1();   
            Console.ReadKey();
        }
        static int Add(int x, int y)
        {
            Console.WriteLine("Add invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x + y;
        }
        static void Test1() {
            Console.WriteLine("main invoke on thread {0}", Thread.CurrentThread.ManagedThreadId);
            BinaryOp op = new BinaryOp(Add);

            IAsyncResult iftAR = op.BeginInvoke(10, 10, null, null);
            Console.WriteLine("Doing more on main");
            Thread.Sleep(3000);
            Console.WriteLine("Doing more on main");

            int sum = op.EndInvoke(iftAR);
            Console.WriteLine("10+10=", sum);
        }
        static void Test2()
        {
            Console.WriteLine("main invoke on thread {0}", Thread.CurrentThread.ManagedThreadId);
            BinaryOp op = new BinaryOp(Add);

            IAsyncResult iftAR = op.BeginInvoke(10, 10, null, null);
            while (!iftAR.IsCompleted) {
                Console.WriteLine("Doing more on main");
                Thread.Sleep(1000);
            }
            Console.WriteLine("IsCompleted {0}", iftAR.IsCompleted);

            int sum = op.EndInvoke(iftAR);
            Console.WriteLine("10+10=", sum);
        }
        static void Test3()
        {
            Console.WriteLine("main invoke on thread {0}", Thread.CurrentThread.ManagedThreadId);
            BinaryOp op = new BinaryOp(Add);

            IAsyncResult iftAR = op.BeginInvoke(10, 10, null, null);
            while (!iftAR.AsyncWaitHandle.WaitOne(1000, true))
            {
                Console.WriteLine("Doing more on main");
            }
            Console.WriteLine("IsCompleted {0}", iftAR.IsCompleted);

            int sum = op.EndInvoke(iftAR);
            Console.WriteLine("10+10=", sum);
        }
        static void AddComplete(IAsyncResult itfAR) {
            Console.WriteLine("AddComplete invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);
            isDone = true;
        }
        static void Test4()
        {
            Console.WriteLine("main invoke on thread {0}", Thread.CurrentThread.ManagedThreadId);
            BinaryOp op = new BinaryOp(Add);

            IAsyncResult iftAR = op.BeginInvoke(10, 10, AddComplete, null);
            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("working.....");
            }

            Console.WriteLine("IsCompleted {0}", iftAR.IsCompleted);

            int sum = op.EndInvoke(iftAR);
            Console.WriteLine("10+10=", sum);
        }
    }
}
