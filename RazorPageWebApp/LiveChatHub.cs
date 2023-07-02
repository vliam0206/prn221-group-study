using Application.IServices;
using Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.SignalR;

public class LiveChatHub : Hub
{
    private IHubContext<LiveChatHub> _context;
    private IClaimService _claimService;
    private IUnitOfWork _unitOfWork;

    public LiveChatHub(IHubContext<LiveChatHub> hubClients, IUnitOfWork unitOfWork, IClaimService claimService)
    {
        _context = hubClients;
        _unitOfWork = unitOfWork;
        _claimService = claimService;
    }

    public async Task SendMessage(string message,Guid groupId)
    {
        if (string.IsNullOrEmpty(message)) return;
        var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId);
        await Clients.All.SendAsync("UserMessage", user,message,groupId);
    }
    public async Task Typing(Guid groupId)
    {
        var user = await _unitOfWork.AccountRepository.GetByIdAsync(_claimService.GetCurrentUserId);
        await Clients.Others.SendAsync("Typing", user,groupId);
    }
}