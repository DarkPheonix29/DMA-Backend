using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DMA_Backend.Hubs;

public class OrderHub : Hub
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("Client verbonden met OrderHub.");
        return base.OnConnectedAsync();
    }
}