using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;
using WiredBrain.Helpers;
using WiredBrain.Models;

namespace WiredBrain.Hubs
{
    //[Authorize]
    public class CoffeeHub: Hub<ICoffeeClient>
    {
        private static readonly OrderChecker _orderChecker =
            new OrderChecker(new Random());
        //[Authorize(Roles = "admin")]
        public async Task GetUpdateForOrder(Order order)
        {
            await Clients.Others.NewOrder(order);
            UpdateInfo result;
            do
            {
                result = _orderChecker.GetUpdate(order);
                await Task.Delay(700);
                if (!result.New) continue;

                await Clients.Caller.ReceiveOrderUpdate(result);
                await Clients.Group("allUpdateReceivers").ReceiveOrderUpdate(result);
            } while (!result.Finished);
            await Clients.Caller.Finished(order);
        }

        public override Task OnConnected()
        {
            if (Context.QueryString["group"] == "allUpdates")
                Groups.Add(Context.ConnectionId, "allUpdateReceivers");
            return base.OnConnected();
        }
    }
}