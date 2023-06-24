using Application.Commons;
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

public class PostRepository
{
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;

    public PostRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task<bool> AddPostAsync(Guid? groupId, string content)
    {
        var post = new Post()
        {
            Content = content,
            GroupId = groupId.Value,
            CreationDate = DateTime.UtcNow,
            AccountCreatedID = Guid.Parse("18B2128E-1852-424B-BF32-AB977085A560")
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
        return _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
    }
    public async Task<Pagination<Post>?> GetAllPostFromGroupAsync(Guid groupId, int pageIndex=0, int pageSize=10)
    {
        var totalPostsCount = await _context.Posts.CountAsync(x => x.GroupId == groupId);
        var posts = await _context.Posts.Where(x => x.GroupId == groupId)
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
