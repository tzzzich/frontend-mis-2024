namespace MyWebApp.Models
{
    public class IcdRootsReportRecordModel
    {
        public string PatientName { get; set; }

        public DateTime? PatientBirthdate { get; set; }

        public string Gender { get; set; }

        public Dictionary<string, int> VisitsByRoot { get; set; }
    }
}
