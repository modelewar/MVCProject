using System.ComponentModel.DataAnnotations;

namespace Demo.PL.viewModels
{
    public class ResetPasswordViewModel
    {
        [Required (ErrorMessage=" New Password Is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = " New Confirm Password Is Required")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
