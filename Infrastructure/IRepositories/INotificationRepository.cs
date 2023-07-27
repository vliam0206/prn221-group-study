using Application.Commons;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<Pagination<Notification>> GetAllNotification(Guid userId, int pageIndex, int pageSize);
    Task<Pagination<Notification>> GetAllUnreadNotificationPagination(Guid userId, int pageIndex, int pageSize);
}
