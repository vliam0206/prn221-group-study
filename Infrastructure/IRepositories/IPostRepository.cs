using Application.Commons;
using Domain.Entities.Posts;

namespace Infrastructure.IRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<bool> AddPostAsync(Guid? groupId, string content);
        Task<bool> EditPostAsync(Post? post);
        Task<Pagination<Post>?> GetAllPostFromGroupAsync(Guid groupId, int pageIndex = 0, int pageSize = 10);
        Task<Pagination<Post>?> GetAllApprovedPostFromGroupAsync(Guid groupId, int pageIndex = 0, int pageSize = 10);
        Task<Post?> GetPostByIdAsync(Guid postId);
    }
}