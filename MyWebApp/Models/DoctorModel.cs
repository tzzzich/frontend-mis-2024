using Microsoft.AspNetCore.Mvc;
using MyWebApp.Utils;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyWebApp.Models
{
    public class DoctorModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage = "Поле Имя не должны быть пустым")]
        [StringLength(1000, MinimumLength = 1)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле дата рождения не должно быть пустым")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата рождения")]
        [DateValidation(ErrorMessage = "Дата рождения должна быть не ранее чем 100 лет назад и не позднее текущей даты.")]
        public DateTime? Birthday { get; set; }

        [Required]
        [RegularExpression("Male|Female")]
        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Phone]
        [RegularExpression(@"^(\+7|8)\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Неправильный формат номера телефона")]
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }
    }
}
