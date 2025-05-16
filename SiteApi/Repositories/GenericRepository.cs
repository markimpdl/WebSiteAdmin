﻿
using Microsoft.EntityFrameworkCore;
using SiteApi.Data;

namespace SiteApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApiDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApiDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(T entity) =>  _dbSet.AddAsync(entity);
        public void  Update(T entity) =>  _dbSet.Update(entity);
        public void  Delete(T entity) => _dbSet.Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();


    }
}
