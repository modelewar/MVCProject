using System.ComponentModel.DataAnnotations;

namespace Demo.PL.viewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Name Is Required")]
		[EmailAddress(ErrorMessage = "Invalide Email")]
		public string Email { get; set; }
	}
}
