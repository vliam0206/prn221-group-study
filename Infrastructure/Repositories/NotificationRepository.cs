using Application.Commons;
using Application.IServices;
using DataAccess;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Pagination<Notification>> GetAllUnreadNotificationPagination(Guid userId, int pageIndex, int pageSize)
    {
        IQueryable<Notification> query = _dbContext.Notifications
                    .Where(x => x.AccountRecievedId == userId
                            && x.Status == Domain.Enums.NotiStatusEnum.Unread)
                    .Where(x=>x.IsDeleted == false);
        return await ToPagination(query, pageIndex, pageSize); ;
    }
    public  async Task<Pagination<Notification>> GetAllNotification(Guid userId, int pageIndex, int pageSize)
    {
        IQueryable<Notification> query = _dbContext.Notifications
                    .Where(x => x.AccountRecievedId == userId && x.IsDeleted == false);
        return await ToPagination(query, pageIndex, pageSize); ;
    }

    public bool HadNotify(Guid receivedId, Guid sendId, NotiTypeEnum type)
    {
        return _dbContext.Notifications.AsNoTracking().Any(x => x.FromAccountId == sendId && x.AccountRecievedId == receivedId && x.Type == type && x.IsDeleted == false);

    }
}
