using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace collections
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayListTest();
            GenericList();

            int a = 100;
            int b = 200;
            SwapTest<int>(ref a, ref b);
            Console.WriteLine("{0}, {1}", a, b);

            string x = "100Y";
            string y = "200Y";
            SwapTest<string>(ref x, ref y);
            Console.WriteLine("{0}, {1}", x, y);


            Console.ReadLine();
        }

        private static void Swap<T>(T a)
        {
            throw new NotImplementedException();
        }

        static void ArrayListTest() {
            ArrayList strAry = new ArrayList();
            strAry.AddRange(new string[] { "abc", "xyz" });
            strAry.Add("haha");
            strAry.Add(new string[] { "dada", "papa" });

            foreach (var v in strAry)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine(strAry.Count);
            Console.WriteLine();
        }
        static void GenericList() {
            List<int> intAry = new List<int>();
            intAry.Add(500);
            intAry.AddRange(new List<int> {100, 200 });
            //intAry.Add("err");

            foreach (var v in intAry)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine(intAry[0]);
            Console.WriteLine(intAry.Count);
            Console.WriteLine();
        }

        static void SwapTest<T>(ref T a, ref T b) {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }
}
