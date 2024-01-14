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

        [Required(ErrorMessage = "Поле имя должно быть заполнено")]
        [Display(Name = "ФИО")]
        public string fullName { get; set; }

        [Display(Name = "Номер телефона")]
        [RegularExpression(@"^(\+7|8)\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Неправильный формат номера телефона")]
        public string phoneNumber { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [Required(ErrorMessage = "Некорректный адрес")]
        [EmailAddress(ErrorMessage = "Поле Email должно быть заполнено")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Дата рождения должна быть не ранее чем 100 лет назад и не позднее текущей даты.")]
        public DateTime birthDate { get; set; }
    }
}
