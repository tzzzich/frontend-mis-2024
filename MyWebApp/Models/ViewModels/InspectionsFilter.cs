namespace MyWebApp.Models.ViewModels
{
    public class InspectionsFilter
    {
        public bool? Grouped { get; set; }
        public string[] IcdRoots { get; set; }
        public int? Size { get; set; } 
        public int? Page {  get; set; }

    }
}
