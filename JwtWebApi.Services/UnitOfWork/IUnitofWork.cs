using Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Repository.UnitOfWork2
{
    public interface IUnitofWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;
        void Save();
    }
}
