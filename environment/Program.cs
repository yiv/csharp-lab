using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace environment
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string v in Environment.GetLogicalDrives()) {
                Console.WriteLine(v);
            }

            Console.WriteLine("OS version: {0}", Environment.OSVersion);
            Console.WriteLine(".Net version: {0}", Environment.Version);
            Console.WriteLine("Processer Count: {0}", Environment.ProcessorCount);


            Console.ReadLine();
        }
    }
}
