using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace delegate1
{
    public delegate int BinaryOp(int x, int y);
    public delegate void DoingThing();
    class Program
    {
        static void Main(string[] args)
        {

            UseDelegate();
           

            Console.ReadKey();
        }
        static int Add(int x, int y) {
            Console.WriteLine("Add invoked on thread {0} + {1}", x, y);
            return x + y;
        }
        static int Sub(int x, int y)
        {
            Console.WriteLine("Sub invoked on thread {0} - {1}", x, y);
            return x - y;
        }
        static void UseDelegate() {
            BinaryOp op = new BinaryOp(Add);

            foreach (var v in op.GetInvocationList())
            {
                Console.WriteLine("Method {0}", v.Method);
                Console.WriteLine("GetType {0}", v.GetType());
                Console.WriteLine("Target {0}", v.Target);
            }
            Console.WriteLine();

            op += Sub;
            foreach (var v in op.GetInvocationList())
            {
                Console.WriteLine("Method {0}", v.Method);
                Console.WriteLine("GetType {0}", v.GetType());
                Console.WriteLine("Target {0}", v.Target);
            }
            Console.WriteLine();

            op -= Add;
            foreach (var v in op.GetInvocationList())
            {
                Console.WriteLine("Method {0}", v.Method);
                Console.WriteLine("GetType {0}", v.GetType());
                Console.WriteLine("Target {0}", v.Target);
            }
            Console.WriteLine();



            foreach (var v in op.GetInvocationList())
            {
                Console.WriteLine("Method {0}", ((BinaryOp)v)(10, 100));
            }
            Console.WriteLine();

        }

    }
}
