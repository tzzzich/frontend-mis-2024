using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class SpecialityModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
