using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionShortModel
    {
        [Required(ErrorMessage = "Поле 'id' является обязательным.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле 'createTime' является обязательным.")]
        public DateTime CreateTime { get; set; }

        [Required(ErrorMessage = "Поле 'date' является обязательным.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Поле 'diagnosis' является обязательным.")]
        public DiagnosisModel Diagnosis { get; set; }
    }
}
