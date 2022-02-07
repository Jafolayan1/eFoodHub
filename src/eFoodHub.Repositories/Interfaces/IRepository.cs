namespace eFoodHub.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(object id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(object id);

        int SaveChanges();
    }
}