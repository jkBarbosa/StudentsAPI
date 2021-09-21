using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<long> Create(TEntity obj);

        Task<TEntity> GetById(long id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Update(TEntity obj);

        Task Delete(long Id);
    }
}
