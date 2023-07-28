using Application.Commons;
using Application.IServices;
using AutoMapper;
using DataAccess;
using Domain.Entities.Posts;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Posts;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    private readonly AppDBContext _context;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;

    public PostRepository(IClaimService claimService, IMapper mapper) : base(claimService)
    {
        _context = new AppDBContext();
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
        _context.Entry<Post>(post).State = EntityState.Modified;
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<Post?> GetPostByIdAsync(Guid postId)
    {
        return GetQuery().FirstOrDefaultAsync(x => x.Id == postId);


    }
    public async Task<Pagination<Post>?> GetAllPostFromGroupAsync(Guid groupId, int pageIndex = 0, int pageSize = 10)
    {
        var query = GetQuery().Where(x => x.GroupId == groupId && x.IsDeleted == false);
        var totalPostsCount = await query.CountAsync();
        var posts = await query
                        .Skip((pageIndex) * pageSize)
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

    public async Task<Pagination<Post>?> GetAllPostFromGroupSearchAsync(Guid groupId, string searchValue, int pageIndex = 0, int pageSize = 10) {
        var query = GetQuery().Where(x => x.GroupId == groupId && x.IsDeleted == false).Where(x => x.Topic.Contains(searchValue));
        var totalPostsCount = await query.CountAsync();
        var posts = await query
                        .Skip((pageIndex) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        var pagination = new Pagination<Post> {
            TotalItemsCount = totalPostsCount,
            PageSize = pageSize,
            PageIndex = pageIndex,
            Items = posts
        };
        return pagination;
    }

    public async Task<Pagination<Post>?> GetAllApprovedPostFromGroupAsync(Guid groupId, int pageIndex = 0, int pageSize = 10)
    {
        var query = GetQuery().Where(x => x.GroupId == groupId && x.IsDeleted == false
                                        && x.Status == Domain.Enums.PostStatusEnum.Approved);
        var totalPostsCount = await query.CountAsync();
        var posts = await query
                        .Skip((pageIndex) * pageSize)
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

    public async Task<Pagination<Post>?> GetAllApprovedPostFromGroupSearchAsync(Guid groupId, string searchValue, int pageIndex = 0, int pageSize = 10) {
        var query = GetQuery().Where(x => x.GroupId == groupId && x.IsDeleted == false
                                        && x.Status == Domain.Enums.PostStatusEnum.Approved 
                                        && x.Topic.Contains(searchValue)
                                        );
        var totalPostsCount = await query.CountAsync();
        var posts = await query
                        .Skip((pageIndex) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        var pagination = new Pagination<Post> {
            TotalItemsCount = totalPostsCount,
            PageSize = pageSize,
            PageIndex = pageIndex,
            Items = posts
        };
        return pagination;
    }
    private IQueryable<Post> GetQuery()
    {
        return _context.Posts.Where(x => x.IsDeleted == false).AsNoTracking()
                                     .Include(x => x.Comments).ThenInclude(x => x.AccountCreated)
                                     .Include(x => x.AccountCreated)
                                     .Include(x => x.Likes)
                                     .Include(x => x.TagInPosts)
                                     .ThenInclude(x => x.Tag)
                                     .Include(x => x.Attachments)
                                     .OrderByDescending(x => x.CreationDate);
    }
}
