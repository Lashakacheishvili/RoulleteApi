using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoulleteApi.Core;
using RoulleteApi.Repository.Ef;
using RoulleteApi.Repository.Interfaces;

namespace RoulleteApi.Repository.Implementation
{
    public class BaseRepository<T, IdType> : IBaseRepository<T, IdType>
        where IdType : IComparable
        where T : class, IBaseEntity<IdType>
    {
        protected readonly RoulleteDbContext _context;
        public BaseRepository(RoulleteDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(c => !c.IsDeleted);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(c => !c.IsDeleted).Where(expression);
        }

        public virtual IdType Create(T entity)
        {
            var created = _context.Set<T>().Add(entity);
            return created.Entity.Id;
        }

        public virtual void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
        }

        public virtual void Delete(IdType id)
        {
            var entity = GetById(id);

            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = true;

            _context.Set<T>().Update(entity);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual T GetById(IdType id, params string[] includes)
        {
            var set = _context.Set<T>().Where(c => !c.IsDeleted && c.Id.Equals(id));

            foreach (var include in includes)
            {
                set = set.Include(include);
            }

            return set.FirstOrDefault();
        }

        public virtual bool Exists(IdType id)
        {
            return _context.Set<T>().Count(c => c.Id.Equals(id) && !c.IsDeleted) > 0;
        }

        public virtual bool NotExists(IdType id)
        {
            return _context.Set<T>().Count(c => c.Id.Equals(id) && !c.IsDeleted) == 0;
        }
    }
}
