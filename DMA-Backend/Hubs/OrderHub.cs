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
    public async Task JoinTableGroup(string tableId)
    {
        var groupName = $"table-{tableId}";
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        Console.WriteLine($"Client {Context.ConnectionId} toegevoegd aan groep {groupName}");
    }
    
}