using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class ConsultationModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public string InspectionId { get; set; }

        [Required]
        public SpecialityModel Speciality { get; set; }

        public List<CommentModel> Comments { get; set; }
    }
}
