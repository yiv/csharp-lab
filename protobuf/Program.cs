using System;
using Msgpb;
using System.IO;
using Google.Protobuf;

namespace protobuf
{
    class Program {
        static void Main(string[] args) {
            byte[] bytes = MarshalToBytes();
            UnmarshalFromBytes(bytes);

            Console.ReadKey();
        }

        static byte[] MarshalToBytes() {
            byte[] bytes;
            ConnectReq connectReq = new ConnectReq { Uid = 55555 };
            using (MemoryStream stream = new MemoryStream()) {
                connectReq.WriteTo(stream);
                bytes = stream.ToArray();

            }
            string str = connectReq.ToString();

            Console.WriteLine("edwin #32 {0}", str);

            foreach (var i in bytes) {
                Console.WriteLine(i);
            }
            return bytes;
        }
        static void UnmarshalFromBytes(byte[] bytes) {
            ConnectReq connectReq = ConnectReq.Parser.ParseFrom(bytes);
            Console.WriteLine(connectReq.Uid);
        }
    }
}
