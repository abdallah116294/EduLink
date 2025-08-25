using EduLink.Core.IRepositories;
using EduLink.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EduLinkDbContext _context;
        private readonly Dictionary<Type, object> _repository;

        public UnitOfWork(EduLinkDbContext context)
        {
            _context = context;
            _repository = new Dictionary<Type, object>();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async  ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            //type of repository
            var type = typeof(TEntity);
            if (!_repository.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<TEntity>(_context);
                _repository[type] = repositoryInstance;
            }
            return (GenericRepository<TEntity>)_repository[type];
        }
    }
}
