using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RoulleteApi.Core;

namespace RoulleteApi.Repository.Interfaces
{
    public interface IBaseRepository<T, IdType>
        where T : class, IBaseEntity<IdType>
        where IdType : IComparable
    {
        IQueryable<T> GetAll();
        T GetById(IdType id, params string[] includes);
        bool Exists(IdType id);
        bool NotExists(IdType id);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        IdType Create(T entity);
        void Update(T entity);
        void Delete(IdType id);
        Task<int> SaveChangesAsync();
    }
}
