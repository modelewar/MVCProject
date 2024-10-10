using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRedpository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCDbContext _dbContext;

        public GenericRedpository(MVCDbContext dbContext) //Ask CLR For Creating  Object
        {
            _dbContext = dbContext;
        }
        public int Add(T item)
        {
            _dbContext.Set<T>().Add(item);
            return _dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            _dbContext.Remove(item);
            return _dbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(T item)
        {
            _dbContext.Update(item);
            return _dbContext.SaveChanges();
        }
    }

}
