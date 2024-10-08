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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private MVCDbContext _dbContext;
        public DepartmentRepository(MVCDbContext dbContext) //ASK clr For Object From DbContext
        {
            _dbContext = dbContext;
        }
        public int Add(Department department)
        {
            _dbContext.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Remove(department);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
           =>_dbContext.Departments.ToList();

 

        public Department GetById(int Id)
        {
          //var department = _dbContext.Departments.Local.Where(D=> D.Id == id).FirstOrDefault();
          //  if(department == null)
          //  {
          //      department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
          //  }
          //  return department;

            return _dbContext.Departments.Find(Id);
        }

        public int Update(Department department)
        {
             _dbContext.Update(department);
             return _dbContext.SaveChanges();
        }
    }
}
