using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class TokenResponseModel
    {
        [Required(ErrorMessage = "Поле 'token' является обязательным.")]
        [MinLength(1, ErrorMessage = "Поле 'token' должно содержать минимум 1 символ.")]
        public string Token { get; set; }
    }
}
