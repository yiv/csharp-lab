using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace file
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWriteAllBytes();

            Console.ReadKey();
        }
        public static void ReadFileUsingReader(string fileName)
        {
            try {
                var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException e) {
                Console.WriteLine("file not found");
                Console.WriteLine(e);
            }
            
        }
        public static void WriteFileUsingWriter(string fileName, string[] lines)
        {
            var outputStream = File.OpenWrite(fileName);
            using (var writer = new StreamWriter(outputStream))
            {
                byte[] preamble = Encoding.UTF8.GetPreamble();
                outputStream.Write(preamble, 0, preamble.Length);
                writer.Write(lines);
            }
        }
        public static void TestReadAllBytes() {
            try
            {
                var fileName = "test.txt";
                var bytes = File.ReadAllBytes(fileName);
                var str = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(str);
            } catch (FileNotFoundException e) {
                Console.WriteLine(e);
            }
        }
        public static void TestWriteAllBytes()
        {
            try
            {
                var fileName = "test.txt";
                var bytes = File.ReadAllBytes(fileName);
                var str = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(str);

                File.WriteAllBytes("test2.txt", bytes);

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
