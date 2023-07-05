using Application.Commons;
using Application.IServices;
using AutoMapper;
using DataAccess;
using Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Comments;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;

    public CommentRepository(IClaimService claimService) : base(claimService)
    {
        _context = new AppDBContext();
    }
    public async Task<bool> AddCommentAsync(Comment comment)
    {
        await _context.AddAsync(comment);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> EditCommentAsync(Comment? comment)
    {
        if (comment == null) return false;
        _context.Update(comment);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<Comment?> GetCommentByIdAsync(Guid commentId)
    {
        return _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
    }
    public async Task<Pagination<Comment>?> GetAllCommentFromPostAsync(Guid postId, int pageIndex = 0, int pageSize = 10)
    {
        IQueryable<Comment> commentsQuery = _context.Comments.Where(x => x.PostId == postId).Include(x => x.ReplyComments);
        return await ToPaginationAsync(commentsQuery, pageIndex, pageSize);
    }
    public async Task<Pagination<Comment>?> ToPaginationAsync(IEnumerable<Comment> commentsList, int pageIndex, int pageSize)
    {
        int totalCommentsCount;
        List<Comment> comments;
        if (commentsList is IQueryable<Comment> query)
        {
            totalCommentsCount = await query.CountAsync();
            comments = await query.Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
        }
        else
        {
            totalCommentsCount = commentsList.Count();
            comments = commentsList.Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();
        }
        var pagination = new Pagination<Comment>
        {
            TotalItemsCount = totalCommentsCount,
            PageSize = pageSize,
            PageIndex = pageIndex,
            Items = comments
        };
        return pagination;
    }
}
