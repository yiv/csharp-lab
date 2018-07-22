using System;
using System.Text;
namespace convert
{
    class Program
    {
        static void Main(string[] args)
        {
            String2Bytes();
            Console.ReadKey();
        }
        public static void Bytes2String() {
            byte[] byteArray = new byte[] {88 };
            string str = Encoding.ASCII.GetString(byteArray);
            Console.WriteLine(str);
        }
        public static void String2Bytes()
        {
            string str = "haha";
            var bytes = Encoding.Default.GetBytes(str);
            foreach (var b in bytes) {
                Console.WriteLine((char)b);
            }

            
        }
    }
}
