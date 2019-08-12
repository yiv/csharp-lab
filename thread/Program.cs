using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace thread
{

    class Params {
        public string Name;
        public int Age;
        public Params(string name = "edwin", int age = 5) {
            Name = name;
            Age = age;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            UseThread();

            UseThreadWithParam();
            Console.ReadLine();
        }

        static void UseThread() {
            var newThread = new Thread(Printer);
            newThread.Start();
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (var x = 0; x < 10; x++)
            {
                Console.WriteLine("T{0} run {1}", threadId, x);
                Thread.Sleep(1000);
            }
        }

        static void UseThreadWithParam()
        {
            var data = new Params();
            var newThread = new Thread(new ParameterizedThreadStart(PrinterWithParam));
            newThread.Start(data);
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (var x = 0; x < 10; x++)
            {
                Console.WriteLine("T{0} run {1}", threadId, x);
                Thread.Sleep(1000);
            }
        }

        static void Printer() {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (var x = 0; x < 10; x++){
                Console.WriteLine("T{0} run {1}", threadId, x);
                Thread.Sleep(1000);
            }
        }
        static void PrinterWithParam(Object obj) {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (var x = 0; x < 10; x++)
            {
                var param = obj as Params;
                Console.WriteLine("T{0} run {1}, name = {2}, age = {3}", threadId, x, param.Name, param.Age);
                Thread.Sleep(1000);
            }
        }
    }
}
