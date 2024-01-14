using Microsoft.AspNetCore.Mvc;
using MyWebApp.Utils;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class DoctorRegisterModel
    {
        [Required (ErrorMessage = "Поле Имя не должны быть пустым")]
        [StringLength(1000, MinimumLength = 1)]
        [Display(Name ="Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Пароль не должны быть пустым")]
        [RegularExpression(@".*[0-9]+.*", ErrorMessage = "Пароль должен содеражть минимум 1 цифру")]
        [StringLength(10000, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть не меньше 6 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Email не должны быть пустым")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Поле дата рождения не должно быть пустым")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата рождения")]
        [DateValidation(ErrorMessage = "Дата рождения должна быть не ранее чем 100 лет назад и не позднее текущей даты.")]
        public DateTime? Birthday { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public String Gender { get; set; }

        [Phone]
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Пожалуйста выберите специальность из выпадающего списка")]
        [HiddenInput]
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            ErrorMessage = "Пожалуйста выберите специальность из выпадающего списка")]
        [Display(Name = "Специальность")]
        public string Speciality { get; set; }
    }
}
