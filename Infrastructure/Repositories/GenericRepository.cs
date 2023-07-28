using Application.Commons;
using Application.IServices;
using DataAccess;
using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDBContext _dbContext;
    private readonly IClaimService _claimService;

    public GenericRepository(IClaimService claimService)
    {
        _dbContext = new AppDBContext();
        _claimService = claimService;
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _dbContext.Set<T>().CountAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>()
            .OrderByDescending(x => x.CreationDate)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        return await includes.Aggregate(_dbContext.Set<T>().AsNoTracking(), (a, b) => a.Include(b)).FirstOrDefaultAsync(x => x.Id == id) ;
    }
     
    public async Task RemoveAsync(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        entity.DeletedBy = _claimService.GetCurrentUserId;
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }
    public async Task RemoveAsyncId(Guid id)
    {
        var entity = await this.GetByIdAsync(id);
        if (entity == null) throw new InvalidOperationException("Id not found");
        await this.RemoveAsync(entity);
    }
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }    

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry<T>(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Pagination<T>> ToPagination(int pageIndex, int pagesize)
    {
        var itemCount = await _dbContext.Set<T>().CountAsync();
        var items = await _dbContext.Set<T>()
                              .Skip(pageIndex * pagesize)
                              .Take(pagesize)
                              .AsNoTracking()
                              .OrderByDescending(x => x.CreationDate)
                              .ToListAsync();
        return new Pagination<T>
        {
            Items = items,
            PageIndex = pageIndex,
            PageSize = pagesize,
            TotalItemsCount = itemCount
        };
    }
    public async Task<Pagination<T>> ToPagination(IQueryable<T> query, int pageIndex, int pagesize)
    {
        var itemCount = await query.CountAsync();
        var items = await query
                              .Skip(pageIndex * pagesize)
                              .Take(pagesize)
                              .AsNoTracking()
                              .ToListAsync();
        return new Pagination<T>
        {
            Items = items,
            PageIndex = pageIndex,
            PageSize = pagesize,
            TotalItemsCount = itemCount
        };
    }
    public async Task SoftDeleteAsync(T entity, Guid userId)
    {
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        entity.DeletedBy = userId;
        await this.UpdateAsync(entity);
    }
    public async Task RemoveRangeAsync(List<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        await _dbContext.SaveChangesAsync() ;
    }
}
