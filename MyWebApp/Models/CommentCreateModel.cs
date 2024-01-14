using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class CommentCreateModel
    {
        
        [Required(ErrorMessage = "Комментарий не может быть пустым")]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        public string ParentId { get; set; }
    }
}
