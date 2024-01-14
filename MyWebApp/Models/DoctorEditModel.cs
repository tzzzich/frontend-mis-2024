using MyWebApp.Utils;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class DoctorEditModel
    {
        [Required]
        [MinLength(1)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле дата рождения не должно быть пустым")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата рождения")]
        [DateValidation(ErrorMessage = "Дата рождения должна быть не ранее чем 100 лет назад и не позднее текущей даты.")]
        public DateTime? Birthday { get; set; }

        [Required]
        [RegularExpression("Male|Female")]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Phone]
        [RegularExpression(@"^(\+7|8)\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Неправильный формат номера телефона")]
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }
    }
}
