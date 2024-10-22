using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.viewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		public UserManager<ApplicationUser> _userManager { get; }

        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager , IMapper mapper)
        {
			_userManager=userManager;
            _mapper = mapper;
        }



		public async Task<IActionResult> Index(string Searchvalue)
		{
			if(string.IsNullOrEmpty(Searchvalue))
			{
				var users = await _userManager.Users.Select( U=> new UserViewModel
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result

				}).ToListAsync();
				return View(users);
			}
            else
            {
                var user = await _userManager.FindByNameAsync(Searchvalue);

                if (user == null)
                {
                    // User was not found, handle this scenario
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(new List<UserViewModel>()); // Or return a different view/message
                }

                var MappedUser = new UserViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(user).Result.ToList()
                };

                return View(new List<UserViewModel> { MappedUser });
            }

        }


        public async Task<IActionResult> Details(string id , string ViewName = "Details")
		{
			if (id is null)
                return BadRequest();
			 var user = await _userManager.FindByIdAsync(id);
			if ( user is null )
				return NotFound();
			var MappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
			return View(ViewName, MappedUser);
 
        }

        public async Task<IActionResult> Edit(string id )
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model , [FromRoute] string id )
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var User = await _userManager.FindByIdAsync(id);
                    User.FName = model.FName;
                    User.LName = model.LName;
                    User.PhoneNumber = model.PhoneNumber;
                    await _userManager.UpdateAsync( User);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message); 
                }

            }
             
            
                return View(model); 
           
        }

        public async Task<IActionResult> Delete(string id) 
        { 
            return await Details(id,"Delete");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(UserViewModel model , [FromRoute] string id ) 
        {

            if (id!= model.Id)
                return BadRequest();
            try
            {
           
                var user= await _userManager.FindByIdAsync(id);
 
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {

                ModelState.AddModelError(string.Empty,ex.Message);
                return RedirectToAction("Error","Home");
            }
        }
	}
}
