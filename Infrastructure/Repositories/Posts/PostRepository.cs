using Application.Commons;
using Application.IServices;
using AutoMapper;
using DataAccess;
using Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Posts;

public class PostRepository :GenericRepository<Post>, IPostRepository
{
    private readonly AppDBContext _context;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;

    public PostRepository(AppDBContext context, IClaimService claimService, IMapper mapper):base(claimService)
    {
        _context = context;
        _claimService = claimService;
        _mapper = mapper;
    }
    public async Task<bool> AddPostAsync(Guid? groupId, string content)
    {
        var post = new Post()
        {
            Content = content,
            GroupId = groupId.Value,
            CreationDate = DateTime.UtcNow,
            AccountCreatedID = _claimService.GetCurrentUserId,
        };
        await _context.AddAsync(post);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> EditPostAsync(Post? post)
    {
        if (post == null) return false;
        _context.Update(post);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<Post?> GetPostByIdAsync(Guid postId)
    {
        return _context.Posts.Include(x => x.Comments).ThenInclude(x=>x.AccountCreated).Include(x => x.Likes).Include(x => x.TagInPosts).Include(x => x.Attachments).FirstOrDefaultAsync(x => x.Id == postId);
    }
    public async Task<Pagination<Post>?> GetAllPostFromGroupAsync(Guid groupId, int pageIndex = 0, int pageSize = 10)
    {
        var query = _context.Posts.AsNoTracking();
        var totalPostsCount = await query.CountAsync(x => x.GroupId == groupId);
        var posts = await query.Include(x => x.Comments).Include(x => x.AccountCreated).Include(x => x.Likes).Include(x => x.TagInPosts).ThenInclude(x => x.Tag).Include(x => x.Attachments).Where(x => x.GroupId == groupId)
                                  .Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
        var pagination = new Pagination<Post>
        {
            TotalItemsCount = totalPostsCount,
            PageSize = pageSize,
            PageIndex = pageIndex,
            Items = posts
        };
        return pagination;
    }
}
