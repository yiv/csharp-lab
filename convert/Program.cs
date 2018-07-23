using System;
using System.Text;
namespace convert
{
    class Program
    {
        static void Main(string[] args)
        {
            Byte2String();
            Console.ReadKey();
        }
        public static void Byte2String() {
            byte b = 55;
            var str = b.ToString("X2");
            Console.WriteLine(str);
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
