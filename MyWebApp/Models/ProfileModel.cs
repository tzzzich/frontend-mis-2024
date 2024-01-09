using Microsoft.AspNetCore.Mvc;
using MyWebApp.Utils;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class ProfileModel
    {
        [HiddenInput]
        public string id { get; set; }
        
        [HiddenInput]
        public string createTime { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string fullName { get; set; }

        [Display(Name = "Номер телефона")]
        [RegularExpression(@"^(\+7|8)\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Неправильный формат номера телефона")]
        public string phoneNumber { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [Required]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Дата рождения должна быть не ранее чем 100 лет назад и не позднее текущей даты.")]
        public DateTime birthDate { get; set; }
    }
}
