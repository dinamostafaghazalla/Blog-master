using System;
using Blog.API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<NotificationHub, ITypedHubClient> _hubContext;

        public MessageController(IHubContext<NotificationHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public string Post()
        {
            string retMessage;

            try
            {
                _hubContext.Clients.All.BroadcastMessageAsync("dfsdf", "fdsf",
                    "https://code-maze.com/netcore-signalr-angular/", "https://web.whatsapp.com/pp?e=https%3A%2F%2Fpps.whatsapp.net%2Fv%2Ft61.24694-24%2F71089472_681191379046040_6776573367832018944_n.jpg%3Foe%3D5DB316C7%26oh%3D75ae90b4d804afe001fb8d77782ca0af&t=l&u=201552447630%40c.us&i=1571242986");
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}