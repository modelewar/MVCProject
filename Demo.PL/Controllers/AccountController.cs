using Demo.DAL.Models;
using Demo.PL.viewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager= signInManager;
		}
		#region Register
		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FName= model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree
                };

                var Result = await _userManager.CreateAsync(User, model.Password);
                if (Result.Succeeded)

                    return RedirectToAction(nameof(Login));

                else

                    foreach (var error in Result.Errors)

                        ModelState.AddModelError(string.Empty, error.Description);



            }
            return View(model);

        }

        #endregion

        //Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    //Login

                    bool flag = await _userManager.CheckPasswordAsync(user,model.Password);
                    if(flag)
                    {
                        //Login
                         var Result  =await _signInManager.PasswordSignInAsync(user, model.Password,model.RememberMe,false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index","Home");
                        }
					}
                    else
                    {
						ModelState.AddModelError(string.Empty, "Invalid password.");
					}
                }
                else
                {
					ModelState.AddModelError(string.Empty, "User not found.");
				}

            }
			return View(model);
		}

		//Sign Out
		//Forget Passoward
		//Reset Password
	}
}
