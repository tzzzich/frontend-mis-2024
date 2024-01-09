using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class ConsultationCreateModel
    {
        [Required]
        public string SpecialityId { get; set; }

        [Required]
        public InspectionCommentCreateModel Comment { get; set; }
    }
}
