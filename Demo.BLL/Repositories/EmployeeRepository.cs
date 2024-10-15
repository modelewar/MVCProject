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
    public class EmployeeRepository : GenericRedpository<Employee>, IEmployeeRepository
    {
        private readonly MVCDbContext _dbContext;
        public EmployeeRepository(MVCDbContext dbcontext) : base(dbcontext)
        { 

        }

        public IQueryable<Employee> GetEmployewByAddress(string Address)
        {
            return _dbContext.Employees.Where(e => e.Address == Address);
        }
    }
}
