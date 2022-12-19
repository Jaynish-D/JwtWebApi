using JwtWebApi.Data.Context;
using Repository.GenericRepository;

namespace OnlineHotel.Repository.UnitOfWork2
{
    public class UnitofWork : IUnitofWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool v)
        {
            if (!this.disposed)
            {
                if (v)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            IGenericRepository<T> genericRepository = new GenericRepository<T>(_context);
            return genericRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
