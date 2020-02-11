using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WiredBrain.Connections
{
    public class CoffeeConnection: PersistentConnection
    {
        protected override async Task OnReceived(IRequest request, string connectionId, string data)
        {
            await base.OnReceived(request, connectionId, data);
        }

        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return base.OnConnected(request, connectionId);
        }

        protected override bool AuthorizeRequest(IRequest request) 
        { 
            return request.User.Identity.IsAuthenticated; 
        } 
    }
}