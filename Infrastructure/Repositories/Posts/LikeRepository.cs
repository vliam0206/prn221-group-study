using Application.Commons;
using Application.IServices;
using DataAccess;
using Domain.Entities;
using Domain.Entities.Posts;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Posts;

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    private readonly AppDBContext _dbContext;
    public LikeRepository(IClaimService claimService) : base(claimService)
    {
        _dbContext = new AppDBContext();
    }

    public async Task<Like> ToggleLikeAsync(Guid postId, Guid id)
    {
        var like = _dbContext.Likes.FirstOrDefault(x => x.PostId == postId && x.AccountCreatedID == id);
        if (like == null) throw new ArgumentException("Like is not Created");
        if (like.Status == Domain.Enums.LikeStatusEnum.Like)
        {
            like.Status = Domain.Enums.LikeStatusEnum.Unlike;
        }else
        {
            like.Status = Domain.Enums.LikeStatusEnum.Like;
        }
        await base.UpdateAsync(like);
        return like;
    }
}
