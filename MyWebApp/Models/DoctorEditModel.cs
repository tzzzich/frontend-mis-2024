using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class DoctorEditModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
