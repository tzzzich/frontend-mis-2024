using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionEditModel
    {
        [Required(ErrorMessage = "Поле Complaints является обязательным.")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Поле Complaints должно содержать от 1 до 5000 символов.")]
        public string Complaints { get; set; }

        [Required(ErrorMessage = "Поле Treatment является обязательным.")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Поле Treatment должно содержать от 1 до 5000 символов.")]
        public string Treatment { get; set; }

        [Required(ErrorMessage = "Поле Conclusion является обязательным.")]
        public Conclusion Conclusion { get; set; }

        public string NextVisitDate { get; set; }

        public string DeathDate { get; set; }

        [MinLength(1, ErrorMessage = "Требуется хотя бы один диагноз.")]
        public List<DiagnosisCreateModel> Diagnoses { get; set; }
    }
}
