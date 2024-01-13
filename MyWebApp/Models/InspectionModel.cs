using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionModel
    {
        [Required(ErrorMessage = "Поле 'id' является обязательным.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле 'createTime' является обязательным.")]
        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage = "Поле 'date' является обязательным.")]
        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Anamnesis { get; set; }

        [DataType(DataType.MultilineText)]
        public string Complaints { get; set; }

        [DataType(DataType.MultilineText)]
        public string Treatment { get; set; }

        public string Conclusion { get; set; }

        [Display(Name = "Дата следующего визита")]
        public DateTime? NextVisitDate { get; set; }

        [Display(Name = "Дата смерти")]
        public DateTime? DeathDate { get; set; }

        public Guid? BaseInspectionId { get; set; }

        public Guid? PreviousInspectionId { get; set; }

        [Required(ErrorMessage = "Поле 'patient' является обязательным.")]
        public PatientModel Patient { get; set; }

        [Required(ErrorMessage = "Поле 'doctor' является обязательным.")]
        public DoctorModel Doctor { get; set; }

        public DiagnosisModel[] Diagnoses { get; set; }

        public InspectionConsultationModel[] Consultations { get; set; }
    }
}
