using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.IO;
using System.Threading;

namespace WebsocketWithProtobuf
{
    class WebSocketsConnection
    {
        readonly ClientWebSocket _webSocket = new ClientWebSocket();

        public async Task Connect() {
            Console.WriteLine("Connecting to service");
            await _webSocket.ConnectAsync(new Uri("ws://192.168.1.205:10050/"), CancellationToken.None)
                .ConfigureAwait(continueOnCapturedContext: false);
            Console.WriteLine("Connected");
        }

        public async Task ReadDataForever() {
            Console.WriteLine("Reading data");

            var buffer = new ArraySegment<byte>(new byte[1024]);
            var completeMessageStream = new MemoryStream();
            while (_webSocket.State == WebSocketState.Open) {
                Console.WriteLine("receivedMessage 1");
                while (true) {
                    var result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None)
                        .ConfigureAwait(continueOnCapturedContext: false);
                    Console.WriteLine("ReceiveAsync");
                    switch (result.MessageType) {
                        case WebSocketMessageType.Binary:
                            Console.WriteLine("receivedMessage 2");
                            completeMessageStream.Write(buffer.Array, 0, result.Count);
                            break;
                        case WebSocketMessageType.Text:
                            Console.WriteLine("receivedMessage 3");
                            throw new NotSupportedException("Didn't expect a binary message type");
                        case WebSocketMessageType.Close:
                            Console.WriteLine("receivedMessage 4");
                            Console.WriteLine($"WebSocket closed: {result.CloseStatus.Value} {result.CloseStatusDescription}");
                            return;
                    }
                    if (result.EndOfMessage)
                        break;
                }

                //var receivedMessage = Encoding.UTF8.GetString(completeMessageStream.ToArray());
                var receivedMessage = completeMessageStream.ToArray();
                Console.WriteLine($"edwin Received message: '{Utility.GetCodeFromPackage(receivedMessage)}'");
                Console.WriteLine($"edwin Received message: '{Utility.BytesToString(receivedMessage)}'");
                completeMessageStream.SetLength(0);     //Reset the stream
            }

            Console.WriteLine($"State!=Open: {_webSocket.State}");
        }

        public async Task WriteData(byte[] data) {
            if (_webSocket.State != WebSocketState.Open)
                throw new InvalidOperationException($"WebSocket State!=Open: {_webSocket.State}");

            Console.WriteLine($"Writing '{data}'");
            var segment = new ArraySegment<byte>(data);
            await _webSocket.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
