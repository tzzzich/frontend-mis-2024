namespace MyWebApp.Models
{
    public class PatientInspectionsFilter
    {
        public bool? Grouped { get; set; }
        public string[] IcdRoots { get; set; }
        public int? Size { get; set; } 
        public int? Page {  get; set; }

    }
}
