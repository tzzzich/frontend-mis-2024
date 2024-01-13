using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionEditModel
    {
        [Required(ErrorMessage = "Поле Жалобы является обязательным.")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Поле Жалобы должно содержать от 1 до 5000 символов.")]
        public string Complaints { get; set; }

        [Required(ErrorMessage = "Поле Анамнез является обязательным.")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Поле Анамнез должно содержать от 1 до 5000 символов.")]
        public string Anamnesis { get; set; }

        [Required(ErrorMessage = "Поле Лечение является обязательным.")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Поле Лечение должно содержать от 1 до 5000 символов.")]
        public string Treatment { get; set; }

        [Required(ErrorMessage = "Поле Заключение является обязательным.")]
        public string Conclusion { get; set; }

        public DateTime? NextVisitDate { get; set; }

        public DateTime? DeathDate { get; set; }

        [MinLength(1, ErrorMessage = "Требуется хотя бы один диагноз.")]
        public List<DiagnosisCreateModel> Diagnoses { get; set; }
    }
}
