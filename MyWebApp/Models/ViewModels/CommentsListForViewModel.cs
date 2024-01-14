namespace MyWebApp.Models.ViewModels
{
    public class CommentsListForViewModel
    {
        public IEnumerable<CommentItemForViewModel> Comments { get; set; }

        public string ConsultId { get; set; }

        public string UserId { get; set; }

        public string SpecName { get; set; }
    }
}
