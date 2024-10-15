using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeerepository;

        public EmployeeController(IEmployeeRepository employeerepository)
        {
            _employeerepository = employeerepository;


        }

        public IActionResult Index()
        {
            var employees = _employeerepository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeerepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);

        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = _employeerepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            return View(ViewName, employee);

        }

        public IActionResult Edit(int? id)
        {
            //if(id is null)
            //     return BadRequest();
            // var employee = _employeerepository.GetById(id.Value);
            // if(employee is null)
            //     return NotFound();
            // return View(employee);
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeerepository.Update(employee);
                    return RedirectToAction(nameof(Index));

                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }
        
        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                _employeerepository.Delete(employee);
                return RedirectToAction(nameof(Index));

            }
            catch (System.Exception ex )
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }
        }
    }
}
