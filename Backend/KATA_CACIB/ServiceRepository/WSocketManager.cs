namespace KATA_CACIB.ServiceRepository
{
    using System;
    using System.IO;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class WSocketManager
    {
        private readonly ServiceManager _serviceManager;

        public WSocketManager(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // Gère une connexion WebSocket pour l'UUID donné
        public async Task HandleWebSocketAsync(WebSocket webSocket, Guid uuid)
        {
            MemoryStream logStream = _serviceManager.GetStreamByUUID(uuid);

            if (logStream != null)
            {
                await SendLogsToWebSocket(webSocket, logStream);
            }
            else
            {
                await SendMessage(webSocket, "UUID not found.");
            }
        }

        // Envoi les logs en continu via WebSocket
        private async Task SendLogsToWebSocket(WebSocket webSocket, MemoryStream logStream)
        {
            var buffer = new byte[1024 * 4];
            StreamReader reader = new StreamReader(logStream, Encoding.UTF8);

            while (webSocket.State == WebSocketState.Open)
            {
                // Lire une ligne depuis le MemoryStream
                string logLine = await reader.ReadLineAsync();
                if (logLine != null)
                {
                    var encodedMessage = Encoding.UTF8.GetBytes(logLine);
                    await webSocket.SendAsync(new ArraySegment<byte>(encodedMessage, 0, encodedMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    // Pas de nouvelles lignes, attendre avant de relire
                    await Task.Delay(1000);
                }
            }
        }

        // Envoi d'un message via WebSocket
        private async Task SendMessage(WebSocket webSocket, string message)
        {
            var encodedMessage = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(encodedMessage, 0, encodedMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

}
