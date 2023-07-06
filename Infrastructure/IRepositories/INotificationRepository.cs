using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories;

public interface INotificationRepository : IGenericRepository<Notification>
{
    List<Notification> GetAllUnreadNotification(Guid userId, int numNoti);
}
