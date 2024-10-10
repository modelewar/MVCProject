using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly MVCDbContext _dbConytext;
        public EmployeeRepository(MVCDbContext dbConytext)
        {
            _dbConytext = dbConytext;
        }
        public int Add(Employee employee)
        {
            _dbConytext.Add(employee);
           return _dbConytext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
             _dbConytext.Remove(employee);
            return _dbConytext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        => _dbConytext.Employees.ToList();




        public Employee GetById(int id)
        => _dbConytext.Employees.Find(id);
            
      

        public int Update(Employee employee)
        {
           _dbConytext.Employees.Update(employee);
            return _dbConytext.SaveChanges();
        }
    }
}
