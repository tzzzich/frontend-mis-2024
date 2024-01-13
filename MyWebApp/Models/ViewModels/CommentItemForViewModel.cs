namespace MyWebApp.Models.ViewModels
{
    public class CommentItemForViewModel
    {
        public CommentModel Comment { get; set; }

        public List<CommentItemForViewModel> ChildComments { get; set; } = new List<CommentItemForViewModel>();

        public int level;

    }
}
