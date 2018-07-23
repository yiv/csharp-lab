using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rand
{
    class Program
    {
        static void Main(string[] args)
        {
            RandRange();
            Console.ReadKey();
        }
        public static void RandNumber (){
            var rand = new Random();
            for (var i = 0; i < 10; i++) {
                Console.WriteLine(rand.Next());
            }
        }
        public static void RandRange()
        {
            var rand = new Random();
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(rand.Next(5,20));
            }
        }
    }
}
