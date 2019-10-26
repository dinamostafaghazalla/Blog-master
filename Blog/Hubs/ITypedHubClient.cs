using System.Threading.Tasks;

namespace Blog.API.Hubs
{
    public interface ITypedHubClient
    {
        Task BroadcastMessageAsync(string title, string message, string url,string image);
    }
}