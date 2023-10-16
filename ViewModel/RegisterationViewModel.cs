using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class RegisterationViewModel
    {
        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Username Min 4 Max 10")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Phone]
        public string PhoneNumber { set; get; } = "";
        [DataType(DataType.Password)]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password Min 8 Max 32")]
        public string Password { set; get; }

    }
}
