using Application.Commons;
using DataAccess;
using Domain.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDBContext _dbContext;

    public GenericRepository()
    {
        _dbContext = new AppDBContext();
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
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task RemoveAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
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
                              .ToListAsync();
        return new Pagination<T>
        {
            Items = items,
            PageIndex = pageIndex,
            PageSize = pagesize,
            TotalItemsCount = itemCount
        };
    }
}
