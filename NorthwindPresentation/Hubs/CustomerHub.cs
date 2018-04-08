using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace NorthwindPresentation.Hubs
{
    public class CustomerHub : Hub
    {
        
//        public override async Task OnConnectedAsync()
//        {
//             
//        }
//
//        public override async Task OnDisconnectedAsync(Exception ex)
//        {
//            await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "left");
//        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("SendMessage", Context.User.Identity.Name, message);
        }
    }
}