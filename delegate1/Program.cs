using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace delegate1
{
    public delegate int BinaryOp(int x, int y);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("main invoke on thread {0}", Thread.CurrentThread.ManagedThreadId);
            BinaryOp op = new BinaryOp(Add);
            Console.WriteLine("Doing more on main");
            Console.WriteLine("10+10=", op(10,10));
            Console.ReadKey();
        }
        static int Add(int x, int y) {
            Console.WriteLine("Add invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);
            return x + y;
        }
    }
}
