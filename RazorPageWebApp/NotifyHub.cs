namespace RazorPageWebApp;

using Microsoft.AspNetCore.SignalR;

public class NotifyHub : Hub
{
    public async Task SendNotifyOther(string message)
    {
        await Clients.Others.SendAsync("ReceiveNotify", message);
    }
}
