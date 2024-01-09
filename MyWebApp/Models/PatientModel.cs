using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class PatientModel
    {
        [Required(ErrorMessage = "Поле 'id' является обязательным.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле 'createTime' является обязательным.")]
        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage = "Поле 'name' является обязательным.")]
        [MinLength(1, ErrorMessage = "Поле 'name' должно содержать минимум 1 символ.")]
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Поле 'gender' является обязательным.")]
        public string Gender { get; set; }
    }
}
