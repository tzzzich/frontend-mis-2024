using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class LoginCredentialsModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@".*[0-9]+.*", ErrorMessage = "Пароль должен содеражть минимум 1 цифру")]
        [StringLength(10000, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не меньше 6 символов")]
        public string Password { get; set; }
    }
}
