using System;
using System.Collections.Generic;
using System.Text;

namespace TaskRun {
    class Utility
    {
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

        public static UInt32 GetCodeFromPackage(byte[] pkg) {
            UInt32 code;
            byte[] codeBytes = new byte[4];

            Array.Copy(pkg, 0, codeBytes, 0, 4);

            Console.WriteLine("edwin #29 {0}", Utility.BytesToString(codeBytes));

            code = Bytes2Code(codeBytes);

            Console.WriteLine("edwin #33 {0}", code);
            return code;
        }
        public static string BytesToString(byte[] bytes) {
            string str = "{";
            if (bytes.Length > 0) {
                foreach (var b in bytes) {
                    var s = string.Format(" {0},", b);
                    str = String.Concat(str, s);
                }
            }
            
            str = str.TrimEnd(',');
            str = String.Concat(str, "}");

            return str;
        }
        public static byte[] CombineCodeAndData(UInt32 code, byte[] message) {
            byte[] codeBytes = Utility.Code2Bytes(code);

            byte[] data = new byte[codeBytes.Length + message.Length];
            Array.Copy(codeBytes, data, codeBytes.Length);
            Array.Copy(message, 0, data, codeBytes.Length, message.Length);

            return data;
        }
    }
}
