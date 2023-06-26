using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Comments;
using Infrastructure.Repositories.Groups;
using Infrastructure.Repositories.Posts;
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
    GroupRepository GroupRepository { get; }
    PostRepository PostRepository { get; }
}
