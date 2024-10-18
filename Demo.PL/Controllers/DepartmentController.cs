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
        private readonly  IUnitOfWork _unitOfWork;//Aggregation Relationship
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork ,IMapper mapper) //Ask CLR For Creating Object From Class Implement The Interface IDepartmentRepository
        {
            _unitOfWork=unitOfWork;
            _mapper = mapper;
        }

        // BasURL/Department/Index
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var MappedDepartment = _mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(departments);
             
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
                _unitOfWork.DepartmentRepository.Add(MappedDepartment);
                int Result = _unitOfWork.Complete();
                //TempData => Dictionary Object
                //Transfer Data From Action To Action
                if (Result >0)
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
            
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
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
                    _unitOfWork.DepartmentRepository.Update(MappedDepartment);
                    _unitOfWork.Complete();
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
                _unitOfWork.DepartmentRepository.Delete(MappedDepartment);
                _unitOfWork.Complete();
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
