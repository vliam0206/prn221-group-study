namespace RazorPageWebApp;

using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

public class NotifyHub : Hub
{
    public async Task SendNotifyOther(Notification message)
    {
        await Clients.Others.SendAsync("ReceiveNotify", message);
    }
}
