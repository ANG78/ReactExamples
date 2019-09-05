using System.Collections.Generic;

namespace Interfaces.Repository
{
    public interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(string code);

        void Add(TEntity entity);

        void Edit(TEntity entity);
       
    }
}
