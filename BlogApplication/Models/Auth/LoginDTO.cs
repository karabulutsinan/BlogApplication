using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models.Auth
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Password { get; set; }
    }
}
