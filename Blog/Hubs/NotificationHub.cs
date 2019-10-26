using Microsoft.AspNetCore.SignalR;

namespace Blog.API.Hubs
{
    public class NotificationHub : Hub<ITypedHubClient>
    {
    }
}