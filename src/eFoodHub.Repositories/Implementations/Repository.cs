using eFoodHub.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteById(object id)
        {
            TEntity entity = _dbContext.Set<TEntity>().Find(id);
            if (entity is not null)
                _dbContext.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}