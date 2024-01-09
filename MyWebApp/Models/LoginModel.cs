using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
	public class LoginModel
	{
		[Required]
		[EmailAddress(ErrorMessage = "Некорректный адрес")]
		public string email { get; set; }

		[Required]
		[RegularExpression(@".*[0-9]+.*", ErrorMessage = "Пароль должен содеражть минимум 1 цифру")]
		[StringLength(10000, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не меньше 6 символов")]
		public string password { get; set; }
	}
}
