﻿using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWrk : IUnitOfWork ,IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        private readonly MVCDbContext _dbContext;

        public UnitOfWrk(MVCDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose() 
        { 
            _dbContext.Dispose();
        }
    }
}