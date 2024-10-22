using AutoMapper;
using Demo.PL.viewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
                _roleManager = roleManager;
            _mapper= mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var MappedRoles =  _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRoles);
            }
            else
            {
               var Roles = _roleManager.FindByNameAsync(SearchValue);
                var MappedRoles = _mapper.Map<IdentityRole, RoleViewModel>(Roles.Result);
                return View(new List<RoleViewModel>() { MappedRoles });
            }
             
        }
    }
}
