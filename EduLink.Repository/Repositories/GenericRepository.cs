using EduLink.Core.IRepositories;
using EduLink.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EduLinkDbContext _context;

        public GenericRepository(EduLinkDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Set<T>().Attach(entity);

            // Set the state of the entity to Modified. 
            // This will track all properties for changes.
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(int id, Action<T> updateAction)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                updateAction(entity);
                _context.Set<T>().Update(entity);
            }
            else
            {
                throw new ArgumentException($"Entity with id {id} not found.");
            }
        }
    }
}
