using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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