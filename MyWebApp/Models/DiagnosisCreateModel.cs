using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class DiagnosisCreateModel
    {
        [Required]
        public string IcdDiagnosisId { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        public DiagnosisType Type { get; set; }
    }
}
