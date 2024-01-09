using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class DoctorRegisterModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        [Display(Name ="Имя")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@".*[0-9]+.*", ErrorMessage = "Пароль должен содеражть минимум 1 цифру")]
        [StringLength(10000, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не меньше 6 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата рождения")]
        public DateTime? Birthday { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public String Gender { get; set; }

        [Phone]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пожалуйста выберите специальность из выпадающего списка")]
        [HiddenInput]
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            ErrorMessage = "Пожалуйста выберите специальность из выпадающего списка")]
        [Display(Name = "Специальность")]
        public string Speciality { get; set; }
    }
}
