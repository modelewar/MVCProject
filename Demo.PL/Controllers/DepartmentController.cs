using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
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

        // BasURL/Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
    }
}
