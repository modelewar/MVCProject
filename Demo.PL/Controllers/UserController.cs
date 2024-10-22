using Demo.DAL.Models;
using Demo.PL.viewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		public UserManager<ApplicationUser> _userManager { get; }
		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager=userManager;
		}



		public async Task<IActionResult> Index(string searchvalue)
		{
			if(string.IsNullOrEmpty(searchvalue))
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
				var user = await _userManager.FindByEmailAsync(searchvalue);
				var MappedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber =user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
			  return View(new List<UserViewModel> { MappedUser });
			}
		}
	}
}
