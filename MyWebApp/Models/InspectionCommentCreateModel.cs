using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionCommentCreateModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
