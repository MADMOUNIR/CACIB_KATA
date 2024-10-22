using KATA_CACIB.ServiceRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace KATA_CACIB.Controllers
{
    [Route("api/v1/ws")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private readonly ServiceManager _serviceManager;
        private readonly WSocketManager _webSocketManager;


        public WebSocketController(ServiceManager serviceManager , WSocketManager webSocketManager)
        {
            _serviceManager = serviceManager;
            _webSocketManager = webSocketManager;
        }

        [HttpGet("{uuid}")]       
        public async Task<IActionResult> Get(Guid uuid)
        {
            // Si c'est une requête WebSocket, démarrer la gestion WebSocket
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    // Lancer la gestion WebSocket via WebSocketManager
                    await _webSocketManager.HandleWebSocketAsync(webSocket, uuid);
                }
                return new EmptyResult(); // La connexion WebSocket gère les messages, pas besoin de retour HTTP
            }

            // Si ce n'est pas une requête WebSocket, récupérer les logs via HTTP
            MemoryStream logStream = _serviceManager.GetStreamByUUID(uuid);
            if (logStream == null)
            {
                return NotFound($"No logs found for UUID: {uuid}");
            }

            // Lire le contenu du MemoryStream
            logStream.Position = 0; // S'assurer de lire depuis le début du stream
            using (StreamReader reader = new StreamReader(logStream, Encoding.UTF8, leaveOpen: true))
            {
                string logs = reader.ReadToEnd();
                return Ok(logs);
            }
        }





    }
}
