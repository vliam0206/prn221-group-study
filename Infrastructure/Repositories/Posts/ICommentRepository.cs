using Application.Commons;
using Domain.Entities.Posts;
using Infrastructure.IRepositories;

namespace Infrastructure.Repositories.Comments
{
    public interface ICommentRepository:IGenericRepository<Comment>
    {
        Task<bool> AddCommentAsync(Comment comment);
        Task<bool> EditCommentAsync(Comment? comment);
        Task<Pagination<Comment>?> GetAllCommentFromPostAsync(Guid postId, int pageIndex = 0, int pageSize = 10);
        Task<Comment?> GetCommentByIdAsync(Guid commentId);
        Task<Pagination<Comment>?> ToPaginationAsync(IEnumerable<Comment> commentsList, int pageIndex, int pageSize);
    }
}