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
        public async Task Add(T item)
        {
           await _dbContext.Set<T>().AddAsync(item);
            
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
             
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>) await _dbContext.Employees.Include(E=> E.Department).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();   
        }

        public void Update(T item)
        {
            _dbContext.Update(item);
            
        }
    }

}
