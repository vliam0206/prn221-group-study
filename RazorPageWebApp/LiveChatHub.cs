using Application.IServices;
using Domain.Entities;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using RazorPageWebApp;
using RazorPageWebApp.Extensions;

public class LiveChatHub : Hub
{
    private IHubContext<LiveChatHub> _context;
    private IClaimService _claimService;
    private IUnitOfWork _unitOfWork;
    private IHttpContextAccessor _httpContextAccessor;

    public LiveChatHub(IHubContext<LiveChatHub> hubClients, IUnitOfWork unitOfWork, IClaimService claimService, IHttpContextAccessor httpContextAccessor)
    {
        _context = hubClients;
        _unitOfWork = unitOfWork;
        _claimService = claimService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SendMessage(string message, Guid groupId)
    {
        if (string.IsNullOrEmpty(message)) return;

        Console.WriteLine(_claimService.GetCurrentUserId + " Send Message:" + message);
        Console.WriteLine($"To Group {groupId}");
        var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId);
        if (user == null) return;
        SaveSession(message, groupId, user);
        await Clients.All.SendAsync("UserMessage", user, message, groupId);
    }

    public async Task Typing(Guid groupId)
    {
        var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId);
        await Clients.Others.SendAsync("Typing", user, groupId);

        // Delay for 5 seconds
        //await Task.Delay(5000);
    }

    private void SaveSession(string message, Guid groupId, Account? user)
    {
        try
        {
            //Send Message and Save Session
            string liveChatGroupMemory = AppConstants.LiveChatMSG(groupId);
            var messages = _httpContextAccessor.HttpContext?.Session.GetEntity<List<LiveChatMessage>>(liveChatGroupMemory);
            if (messages == null || messages is not List<LiveChatMessage>) messages = new();
            var msg = new LiveChatMessage(user, message);
            messages.Add(msg);
            _httpContextAccessor.HttpContext?.Session.SetEntity(liveChatGroupMemory, messages);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Can't save Live Chat session");
        }
    }

}
public record LiveChatMessage(Account? User, string? Message);