using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class DiagnosisModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        [MinLength(1)]
        public string Code { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
