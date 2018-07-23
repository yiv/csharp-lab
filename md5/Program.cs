using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace md5
{
    class Program
    {
        static void Main(string[] args)
        {
            md5Fun();
            Console.ReadKey();
        }
        public static void md5Fun() {
            var md5Hasher = MD5.Create();
            var str = "abc";
            var md5Bytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
            var strBuilder = new StringBuilder();
            foreach (var b in md5Bytes) {
                strBuilder.Append(b.ToString("x2"));
            }
            Console.WriteLine(strBuilder.ToString());
        }
    }
}
