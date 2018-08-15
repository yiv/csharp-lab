using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytes
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = new byte[] { 0, 0, 39, 117 };
            Console.WriteLine(Bytes2Code(bytes));
            Console.ReadKey();
        }
        public static string BytesToString(byte[] bytes) {
            if (bytes.Length == 0) {
                bytes = new byte[] { 1, 2, 3, 4 };
            }
            string str = "{";
            foreach (var b in bytes) {
                var s = string.Format(" {0},", b);
                str = String.Concat(str, s);
            }
            str = str.TrimEnd(',');
            str = String.Concat(str, "}");

            return str;
        }
        static void ByteArrayAssignment() {
            byte[] a = { 1,2,3};
            byte[] b;
            b = new byte[] { 1, 2,3,4};
        }
        public static void TestPrintBytes() {
            byte[] test = { 1, 2, 3, 5 };
            Console.WriteLine(test);
            string byteStr = Encoding.UTF8.GetString(test);
            Console.WriteLine(byteStr);
        }

        public static byte[] Code2Bytes(UInt32 code) {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(code >> 24);
            bytes[1] = (byte)(code >> 16);
            bytes[2] = (byte)(code >> 8);
            bytes[3] = (byte)(code);
            return bytes;
        }

        public static UInt32 Bytes2Code(byte[] bytes) {
            UInt32 code = 0;
            code = (UInt32)(bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3]);
            return code;
        }
    }
}
