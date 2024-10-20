using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.PL.viewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            var MappedDepartment = _mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(departments);
             
            return View(MappedDepartment);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                await _unitOfWork.DepartmentRepository.Add(MappedDepartment);
                int Result = await _unitOfWork.Complete();
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

        public async Task<IActionResult> Details(int? id , string ViewName = "Details" )
        {
            if (id is null)
              return BadRequest();//Status Code 400
            
            var department = await _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return NotFound();
            var MappedDepartments = _mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, MappedDepartments);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest();

            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();
            //return View(department);
            return await Details(id , "Edit");

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel departmentVM , [FromRoute] int id )
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = _mapper.Map<DepartmentViewModel,Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(MappedDepartment);
                    await _unitOfWork.Complete();
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
        public async Task<IActionResult> Delete(int? id) 
        {
            return await Details(id, "Delete");

        }

        public async  Task<IActionResult> Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
           if(id != departmentVM.Id)
                return BadRequest();
            try
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Delete(MappedDepartment);
                await _unitOfWork.Complete();
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
