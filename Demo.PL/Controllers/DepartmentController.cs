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
            if (ModelState.IsValid) //Server Side Validation
            {
                _departmentRepository.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? id , string ViewName = "Details" )
        {
            if (id is null)
              return BadRequest();//Status Code 400
            var department = _departmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound();
            return View(ViewName,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest();

            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
            return Details(id , "Edit");

        }

        public IActionResult Edit(Department department , [FromRoute] int id )
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
                return View(department);
        }
    }
}
