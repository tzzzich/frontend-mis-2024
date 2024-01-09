using Microsoft.AspNetCore.Mvc;
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

        [Required]
        [MinLength(1)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Дата рождения")]
        public DateTime? Birthday { get; set; }

        [Required]
        [RegularExpression("Male|Female")]
        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(1)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [RegularExpression(@"^(\+7|8)\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Неправильный формат номера телефона")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}
