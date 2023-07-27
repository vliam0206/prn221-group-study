using Application.Commons;
using Application.IServices;
using DataAccess;
using Domain.Entities;
using Domain.Entities.Posts;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public async Task<Like> ToggleLikeAsync(Guid postId, Guid userId)
    {
        var like = _dbContext.Likes.FirstOrDefault(x => x.PostId == postId && x.AccountCreatedID == userId);
        bool is_update = true;

        if (like == null)
        {
            is_update = false;
            like = new Like()
            {
                Status = Domain.Enums.LikeStatusEnum.Like,
                PostId = postId
                ,
                AccountCreatedID = userId
            };
        } else 
        if (like.Status == Domain.Enums.LikeStatusEnum.Like)
        {
            like.Status = Domain.Enums.LikeStatusEnum.Unlike;
        }
        else
        {
            like.Status = Domain.Enums.LikeStatusEnum.Like;
        }
        if (is_update)
            await base.UpdateAsync(like);
        else 
            await base.AddAsync(like);
        return like;
    }
}
