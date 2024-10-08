using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly  IDepartmentRepository _departmentRepository;//Aggregation Relationship

        public DepartmentController(IDepartmentRepository departmentRepository) //Ask CLR For Creating Object From Class Implement The Interface IDepartmentRepository
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
