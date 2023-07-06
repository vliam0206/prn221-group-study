using Domain.Entities.Posts;

namespace Infrastructure.IRepositories
{
    public interface ILikeRepository : IGenericRepository<Like>
    {
        Task ToggleLikeAsync(Guid postId, Guid id);
    }
}