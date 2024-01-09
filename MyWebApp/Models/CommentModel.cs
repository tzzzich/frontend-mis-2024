using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class CommentModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Author { get; set; }

        public string? ParentId { get; set; }
    }
}
