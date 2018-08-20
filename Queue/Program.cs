using System;
using System.Collections;
using System.Collections.Concurrent;

namespace QueueNameSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue();
            Console.ReadKey();
        }
        static void ConcurrentQueue() {
            ConcurrentQueue<int> cq = new ConcurrentQueue<int>();
            cq.Enqueue(55);
            cq.Enqueue(66);
            Console.WriteLine("queue count {0}", cq.Count);
            int res;
            var ok = cq.TryDequeue(out res);
            Console.WriteLine(res);
            ok = cq.TryDequeue(out res);
            Console.WriteLine(res);

            if (cq.TryDequeue(out res)) {
                Console.WriteLine(res);
            } else {
                Console.WriteLine("end");
            }

        }
        static void SimpleQueue() {
            Queue q = new Queue();
            q.Enqueue(5);
            q.Enqueue("6");
            PrintValues(q);

            Console.WriteLine("queue count {0}", q.Count);
            Console.WriteLine(q.Dequeue());
            Console.WriteLine("queue count {0}", q.Count);
            Console.WriteLine(q.Dequeue());
            Console.WriteLine("***************");
            Console.WriteLine("queue count {0}", q.Count);
            //var last = q.Dequeue();
            //if (last != null) {
            //    Console.WriteLine(last);
            //} else {
            //    Console.WriteLine("no more");
            //}
            
        }

        static void PrintValues(Queue q) {
            foreach (var v in q) {
                var t = v.GetType();
                Console.WriteLine(t);
                Console.WriteLine(v);
                Console.WriteLine("*****end*****");
            }
        }
    }
}
