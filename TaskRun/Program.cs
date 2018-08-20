using System;
using System.Threading.Tasks;
using System.Threading;
using Msgpb;
using System.IO;
using Google.Protobuf;

namespace TaskRun
{
    class Program
    {
        private static WebSocketsConnection connection = new WebSocketsConnection();
        static void Main(string[] args)
        {
            var t1 = Task.Run(()=> Connect());
            t1.Wait();
            Console.WriteLine("main t1");

            var t2 = Task.Run(()=> connection.Running());
            Console.WriteLine("main t2");

            var t3 = Task.Run(() => Login());
            Console.WriteLine("main t3");

            var t4 = Task.Run(() => ReadingLoop());
            Console.WriteLine("main t4");
            
            t4.Wait();
        }

        static void Connect() {
            var connectTask = connection.Connect();
        }


        static void HeartBeat() {
            byte[] data;
            HeartBeatReq heartBeatReq = new HeartBeatReq { };
            using (MemoryStream stream = new MemoryStream()) {
                heartBeatReq.WriteTo(stream);
                data = stream.ToArray();
            }
            while (true) {
                Thread.Sleep(2000);
                data = Utility.CombineCodeAndData(10102, new byte[] { });
                connection.Write(data);
            }
            
        }

        static void Login() {
            byte[] data, msg;
            LoginReq loginReq = new LoginReq { Token = "eyJhbGciOiJIUzI1NiIsImtpZCI6ImtpZC1oZWFkZXIiLCJ0eXAiOiJKV1QifQ.eyJleHAiOjE1MzQ4NDMxNTIsInVpZCI6OTg2MzY0MzMxMjk0NzIxfQ.gvXdwFBIZEcbhYq37u4TUmzvJYF7LnLW0v3lteSEjAM" };
            using (MemoryStream stream = new MemoryStream()) {
                loginReq.WriteTo(stream);
                msg = stream.ToArray();
            }
            data = Utility.CombineCodeAndData(10101, msg);
            connection.Write(data);
        }

        static void ReadingLoop() {
            while (true) {
                var data = connection.Read();
                var code = Utility.Bytes2Code(data);
                Console.WriteLine("edwin #58 {0}", code);
                if (code == 10101) {
                    var t = Task.Run(() => HeartBeat());
                }
            }
        }
    }
}
