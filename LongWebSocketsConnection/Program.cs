using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LongWebSockets
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }
        static async Task Run()
        {
            var connection = new LongWebSocketsConnection();
            await connection.Connect();
            var readerTask = connection.ReadDataForever();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                Thread.Sleep(1000);
                await connection.WriteData($"{stopwatch.ElapsedMilliseconds / 1000.0:N1}");
            }
        }
    }
}
