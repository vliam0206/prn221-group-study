using DataAccess;
using Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Posts;

public class PostRepository
{
    private readonly AppDBContext _context;

    public PostRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task<bool> AddPostAsync(Guid? groupId,string content)
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
}
