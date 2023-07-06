using Infrastructure.IRepositories;
using Infrastructure.IRepositories.Groups;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks;

public interface IUnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public ICommentRepository CommentRepository { get; }
    public IGroupRepository GroupRepository { get; }
    public IPostRepository PostRepository { get; }
    public IAccountInGroupRepository AccountInGroupRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    public ILikeRepository LikeRepository { get; }
}
