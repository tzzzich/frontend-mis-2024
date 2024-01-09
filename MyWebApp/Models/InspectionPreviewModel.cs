using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionPreviewModel
    {
        [Required(ErrorMessage = "Поле 'id' является обязательным.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле 'createTime' является обязательным.")]
        public DateTime CreateTime { get; set; }

        public Guid? PreviousId { get; set; }

        [Required(ErrorMessage = "Поле 'date' является обязательным.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Поле 'conclusion' является обязательным.")]
        public string Conclusion { get; set; }

        [Required(ErrorMessage = "Поле 'doctorId' является обязательным.")]
        public Guid DoctorId { get; set; }

        [Required(ErrorMessage = "Поле 'doctor' является обязательным.")]
        [MinLength(1, ErrorMessage = "Поле 'doctor' должно содержать минимум 1 символ.")]
        public string Doctor { get; set; }

        [Required(ErrorMessage = "Поле 'patientId' является обязательным.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "Поле 'patient' является обязательным.")]
        [MinLength(1, ErrorMessage = "Поле 'patient' должно содержать минимум 1 символ.")]
        public string Patient { get; set; }

        [Required(ErrorMessage = "Поле 'diagnosis' является обязательным.")]
        public DiagnosisModel Diagnosis { get; set; }

        public bool HasChain { get; set; }

        public bool HasNested { get; set; }
    }
}
