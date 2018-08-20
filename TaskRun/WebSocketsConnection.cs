using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.IO;
using System.Threading;

namespace TaskRun {
    class WebSocketsConnection
    {
        readonly ClientWebSocket _webSocket = new ClientWebSocket();
        private static BlockQueue<byte[]> _rcvBuf = new BlockQueue<byte[]>();
        private static BlockQueue<byte[]> _sndBuf = new BlockQueue<byte[]>();

        public async Task Connect() {
            Console.WriteLine("Connecting to service");
            await _webSocket.ConnectAsync(new Uri("ws://192.168.1.205:10050/"), CancellationToken.None)
                .ConfigureAwait(continueOnCapturedContext: false);
            Console.WriteLine("Connected");
        }

        public void Running() {
            Console.WriteLine("Running");
            var receive = RcvDataFromBuf();
            var send = SndDataFromBuf();
        }

        private async Task RcvDataFromBuf() {
            Console.WriteLine("RcvDataFromBuf 0");

            var buffer = new ArraySegment<byte>(new byte[1024]);
            var completeMessageStream = new MemoryStream();
            while (_webSocket.State == WebSocketState.Open) {
                Console.WriteLine("RcvDataFromBuf 1");
                while (true) {
                    var result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None)
                        .ConfigureAwait(continueOnCapturedContext: false);
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
                Console.WriteLine("RcvDataFromBuf 2");
                var receivedMessage = completeMessageStream.ToArray();
                _rcvBuf.Enqueue(receivedMessage);
                Console.WriteLine($"edwin Received message: '{Utility.GetCodeFromPackage(receivedMessage)}'");
                Console.WriteLine($"edwin Received message: '{Utility.BytesToString(receivedMessage)}'");
                completeMessageStream.SetLength(0);     //Reset the stream
            }

            Console.WriteLine($"State!=Open: {_webSocket.State}");
        }

        private async Task SndDataFromBuf() {
            Thread.Sleep(1000);
            while (true) {
                Console.WriteLine("SndDataFromBuf 1");
                if (_webSocket.State != WebSocketState.Open) {
                    Console.WriteLine("SndDataFromBuf 2");
                    throw new InvalidOperationException($"WebSocket State!=Open: {_webSocket.State}");
                }
                Console.WriteLine("SndDataFromBuf 3");
                var data = _sndBuf.Dequeue();
                Console.WriteLine("SndDataFromBuf 4");
                var segment = new ArraySegment<byte>(data);
                await _webSocket.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None)
                    .ConfigureAwait(continueOnCapturedContext: false);
                Console.WriteLine("SndDataFromBuf 5");
            }
        }

        public void Write(byte[] data) {
            _sndBuf.Enqueue(data);
        }
        public byte[] Read() {
            var data = _rcvBuf.Dequeue();
            return data;
        }
    }
}
