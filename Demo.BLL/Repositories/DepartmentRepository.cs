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
    public class DepartmentRepository : GenericRedpository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(MVCDbContext dbContext):base(dbContext)
        {
            
        }


    }
}
