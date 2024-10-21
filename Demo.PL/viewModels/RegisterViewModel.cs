using System.ComponentModel.DataAnnotations;

namespace Demo.PL.viewModels
{
	public class RegisterViewModel
	{
        [Required(ErrorMessage ="First Name Is Required")]
        public string FName { get; set; }

		[Required(ErrorMessage = "Last Name Is Required")]
		public string LName { get; set; }
		[Required(ErrorMessage = "Email Name Is Required")]
		[EmailAddress(ErrorMessage = "Invalide Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password Name Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password Name Is Required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
		public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
 
	}
}
