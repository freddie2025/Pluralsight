using System.Web.Http;
using Microsoft.AspNet.SignalR;
using WiredBrain.Hubs;
using WiredBrain.Models;

namespace WiredBrain.Controllers
{
    public class CoffeeController : ApiController
    {
        private static int OrderId;
        [HttpPost]
        public int OrderCoffee(Order order)
        {
             //var hubContext = GlobalHost.ConnectionManager.GetHubContext<CoffeeHub>();
            //hubContext.Clients.All.NewOrder(order);
            //Save order somewhere and get order id
            OrderId++;
            return OrderId; //return order id
        }
    }
}
