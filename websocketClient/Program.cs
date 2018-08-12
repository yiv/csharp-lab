using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace websocketClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            Test1();
            Console.ReadKey();
        }
        public static void Test1()
        {
            using (var nf = new Notifier())
            using (var ws = new WebSocket("ws://echo.websocket.org"))
            {
                // Set the WebSocket events.

                ws.OnOpen += (sender, e) => ws.Send("Hi, there!");

                ws.OnMessage += (sender, e) =>
                    nf.Notify(
                      new NotificationMessage
                      {
                          Summary = "WebSocket Message",
                          Body = !e.IsPing ? e.Data : "Received a ping.",
                          Icon = "notification-message-im"
                      }
                    );

                ws.OnError += (sender, e) =>
                    nf.Notify(
                      new NotificationMessage
                      {
                          Summary = "WebSocket Error",
                          Body = e.Message,
                          Icon = "notification-message-im"
                      }
                    );

                ws.OnClose += (sender, e) =>
                    nf.Notify(
                      new NotificationMessage
                      {
                          Summary = String.Format("WebSocket Close ({0})", e.Code),
                          Body = e.Reason,
                          Icon = "notification-message-im"
                      }
                    );
                // To change the logging level.
                ws.Log.Level = LogLevel.Trace;


                ws.Connect();

                // Connect to the server asynchronously.
                //ws.ConnectAsync ();

                Console.WriteLine("\nType 'exit' to exit.\n");
                while (true)
                {
                    Thread.Sleep(1000);
                    Console.Write("> ");
                    var msg = Console.ReadLine();
                    if (msg == "exit")
                        break;

                    // Send a text message.
                    ws.Send(msg);
                }
            }
        }
    }
}

