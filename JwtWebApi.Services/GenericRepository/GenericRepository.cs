using JwtWebApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly ApplicationDbContext _Context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _Context = dbContext;
            _dbSet = _Context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if(_Context.Entry(entity).State == EntityState.Detached) 
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_Context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            return entity;
        }
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool v)
        {
           if(!this.disposed)
            {
                if(v)
                {
                    _Context.Dispose();
                }
            }
           this.disposed = true;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties != null)
            {
                foreach (var includeProparty in
                    includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProparty);
                }
            }
            if(OrderBy != null)
            {
                return OrderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        //public T GetById(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        //{
        //    return _dbSet.Find(predicate);
        //}

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _Context.Entry(entity).State= EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
