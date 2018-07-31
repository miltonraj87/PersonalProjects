using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Repository.Contract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQueryable();
        IEnumerable<T> GetAll();        
        T FindById(int id);
        IEnumerable<T> Find(Expression<Func<T, Boolean>> where);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
