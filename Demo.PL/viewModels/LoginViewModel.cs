using System.ComponentModel.DataAnnotations;

namespace Demo.PL.viewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Name Is Required")]
		[EmailAddress(ErrorMessage = "Invalide Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password Name Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool  RememberMe { get; set; }
    }
}
