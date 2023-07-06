using Application.IServices;
using DataAccess;
using Domain.Entities;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    private readonly AppDBContext _dbContext;
    public NotificationRepository(IClaimService claimService) : base(claimService)
    {
        _dbContext = new AppDBContext();
    }

    public List<Notification> GetAllUnreadNotification(Guid userId, int numNoti)
    {
        return _dbContext.Notifications
            .Where(x => x.AccountRecievedId == userId
                    && x.Status == Domain.Enums.NotiStatusEnum.Unread)
            .Take(numNoti)
            .ToList();
    }
}
