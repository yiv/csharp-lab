using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KcpProject;

namespace kcp
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new UDPSession();
            conn.Connect("127.0.0.1", 12345);
            Task.Run(()=> {
                Read(conn);
            });
            Task.Run(() => {
                Write(conn);
            });
            Console.ReadLine();
        }
        static void Read(UDPSession conn)
        {
            var buf = new byte[1024];
            while (true)
            {
                var count = conn.Recv(buf, 0, buf.Length);
                if (count < 0)
                {
                    Console.WriteLine("kcp read fail");
                    continue;
                }
                else if (count == 0)
                {

                }
                else
                {
                    var msg = Encoding.UTF8.GetString(buf, 0, count);
                    Console.WriteLine("kcp read success: {0}, {1}", count, msg);
                    Thread.Sleep(1000);
                }
            }
        }

        static void Write(UDPSession conn)
        {
            while (true)
            {
                var msg = Encoding.UTF8.GetBytes(string.Format("hi, kcp {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                var count = conn.Send(msg, 0, msg.Length);
                if (count < 0)
                {
                    Console.WriteLine("kcp write fail");
                }
                else
                {
                    //Console.WriteLine("kcp write success", msg.ToString());
                }
                Thread.Sleep(1000);
            }
        }

    }
}
