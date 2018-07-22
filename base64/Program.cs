using System;
using System.IO;

namespace base64
{
    class Program
    {
        static void Main(string[] args)
        {
            Base64String2bytes();
        }
        public static void Bytes2Base64String() {
            var bytes = File.ReadAllBytes("fb.jpg");
            var base64str = Convert.ToBase64String(bytes);
            Console.WriteLine(base64str);
            Console.ReadKey();
        }
        public static void Base64String2bytes()
        {
            var bytes = File.ReadAllBytes("fb.jpg");
            var base64str = Convert.ToBase64String(bytes);

            var newBytes = Convert.FromBase64String(base64str);

            File.WriteAllBytes("fb2.jpg", newBytes);

            Console.ReadKey();
        }
    }

}
