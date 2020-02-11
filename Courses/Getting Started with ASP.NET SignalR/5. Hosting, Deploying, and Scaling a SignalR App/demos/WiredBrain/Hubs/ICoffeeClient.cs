using System.Threading.Tasks;
using WiredBrain.Models;

namespace WiredBrain.Hubs
{
    public interface ICoffeeClient
    {
        Task NewOrder(Order order);
        Task ReceiveOrderUpdate(UpdateInfo info);
        Task Finished(Order order);
    }
}