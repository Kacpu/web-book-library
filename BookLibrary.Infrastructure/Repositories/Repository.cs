﻿using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id, string[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await Task.FromResult(await query.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<IEnumerable<T>> BrowseAllAsync(Expression<Func<T, bool>> filter, string[] includeProperties,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int? skip, int? take)
        {
            IQueryable<T> query = _dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            if (skip != null && take != null)
            {
                query = query.OrderBy(x => x.Id).Skip(skip.Value).Take(take.Value);
            }

            return await Task.FromResult(await query.ToListAsync());
        }

        public async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _appDbContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(c => c.Id == id);
            _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExist<CheckT>(int id) where CheckT : Entity
        {
            var e = await _appDbContext.FindAsync<CheckT>(id);
            return await Task.FromResult(e is not null);
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
