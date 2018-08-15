using System.Threading;
using System.Threading.Tasks;
using Msgpb;
using Google.Protobuf;
using System.IO;
using System;

namespace WebsocketWithProtobuf
{
    class Program
    {
        static void Main(string[] args) {
            Run().Wait();
        }
        static async Task Run() {
            var connection = new WebSocketsConnection();
            await connection.Connect();
            var readerTask = connection.ReadDataForever();
            byte[] data, message;
            LoginReq loginReq = new LoginReq { Token = "eyJhbGciOiJIUzI1NiIsImtpZCI6ImtpZC1oZWFkZXIiLCJ0eXAiOiJKV1QifQ.eyJleHAiOjE1MzQ0MDk2MjMsInVpZCI6OTg2MzY0MzMxMjk0NzIxfQ.I9hZKcVzG9QLujrFoOjkCaWGwLrVZqI5f_rM-TC-QcE" };
            using (MemoryStream stream = new MemoryStream()) {
                loginReq.WriteTo(stream);
                message = stream.ToArray();
            }
            

            Console.WriteLine("edwin #26 {0}", BytesToString(CombineCodeAndData(10101, message)));

            await connection.WriteData(CombineCodeAndData(10101, message));



            Thread.Sleep(5000);


            HeartBeatReq heartBeatReq = new HeartBeatReq { };
            using (MemoryStream stream = new MemoryStream()) {
                heartBeatReq.WriteTo(stream);
                data = stream.ToArray();
            }

            
            while (true) {
                Thread.Sleep(5000);
                await connection.WriteData(data);
            }
        }

        static byte[] CombineCodeAndData(UInt32 code, byte[] message)  {
            byte[] codeBytes = Utility.Code2Bytes(code);
            
            byte[] data = new byte[codeBytes.Length + message.Length];
            Array.Copy(codeBytes, data, codeBytes.Length);
            Array.Copy(message, 0, data, codeBytes.Length, message.Length);                       

            return data;
        }

        public static string BytesToString(byte[] bytes) {
            string str = "{";
            foreach (var b in bytes) {
                var s = string.Format(" {0},", b);
                str = String.Concat(str, s);
            }
            str = str.TrimEnd(',');
            str = String.Concat(str, "}");

            return str;
        }
    }
}
