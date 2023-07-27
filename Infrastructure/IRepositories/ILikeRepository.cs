using Domain.Entities.Posts;

namespace Infrastructure.IRepositories
{
    public interface ILikeRepository : IGenericRepository<Like>
    {
        Task<Like> ToggleLikeAsync(Guid postId, Guid id);
    }
}