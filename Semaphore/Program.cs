using System;
using System.Threading;
using System.Threading.Tasks;


namespace SemaphoreTest
{
    class Program
    {
        private static Semaphore _pool;

        public static void Main() {
            TestFromOffical();
            Console.ReadKey();
        }

        static void TestFromOffical() {
            _pool = new Semaphore(0, int.MaxValue);

            Task t1 = Task.Run( () => {
                while (true) {
                    Thread.Sleep(5000);
                    Console.WriteLine("edwin #1 {0: yyyy-MM-dd HH:mm:ss:fff}", DateTime.Now);
                    _pool.Release();
                }
            });

            Task t2 = Task.Run(() => {
                while (true) {
                    _pool.WaitOne();
                    Console.WriteLine("edwin #2 {0: yyyy-MM-dd HH:mm:ss:fff}", DateTime.Now);
                }
            });

            t1.Wait();
            t2.Wait();
        }

        
    }
}
