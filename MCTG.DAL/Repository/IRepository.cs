using MCTG.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.DAL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T>? GetAll();
        T? GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
