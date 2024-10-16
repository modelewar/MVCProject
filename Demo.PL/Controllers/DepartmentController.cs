using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.viewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly  IDepartmentRepository _departmentRepository;//Aggregation Relationship
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository ,IMapper mapper) //Ask CLR For Creating Object From Class Implement The Interface IDepartmentRepository
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        // BasURL/Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            var MappedDepartment = _mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(departments);
            ViewData["Message"] = "Message From ViewData";
            return View(MappedDepartment);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                int Result = _departmentRepository.Add(MappedDepartment);
                //TempData => Dictionary Object
                //Transfer Data From Action To Action
                if(Result >0)
                {
                    TempData["Message"] = $"Department {departmentVM.Name} is Created ";
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public IActionResult Details(int? id , string ViewName = "Details" )
        {
            if (id is null)
              return BadRequest();//Status Code 400
            
            var department = _departmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound();
            var MappedDepartments = _mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, MappedDepartments);
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

        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentViewModel departmentVM , [FromRoute] int id )
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = _mapper.Map<DepartmentViewModel,Department>(departmentVM);
                    _departmentRepository.Update(MappedDepartment);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
                return View(departmentVM);
        }
        [HttpGet]
        public IActionResult Delete(int? id) 
        {
            return Details(id, "Delete");

        }

        public IActionResult Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
           if(id != departmentVM.Id)
                return BadRequest();
            try
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _departmentRepository.Delete(MappedDepartment);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex )
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(departmentVM);
            }


        }

    }
}
