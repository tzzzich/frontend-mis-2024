using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class PatientCreateModel
    {
        [Display(Name="Имя")]
        [Required(ErrorMessage = "Поле 'name' является обязательным.")]
        [StringLength(1000, ErrorMessage = "Поле 'Имя' должно содержать не более 1000 символов.")]
        public string Name { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.DateTime)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Поле 'Пол' является обязательным.")]
        public string Gender { get; set; }
    }
}
