using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
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
        public void Add(T item)
        {
            _dbContext.Set<T>().Add(item);
            
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
             
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _dbContext.Employees.Include(E=> E.Department).ToList();
            }
            return _dbContext.Set<T>().ToList();   
        }

        public void Update(T item)
        {
            _dbContext.Update(item);
            
        }
    }

}
