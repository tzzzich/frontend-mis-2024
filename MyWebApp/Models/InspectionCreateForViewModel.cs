namespace MyWebApp.Models
{
    public class InspectionCreateForViewModel
    {
        public InspectionCreateModel Inspection {  get; set; }

        public PatientModel Patient { get; set; }

        public bool IsSecond {  get; set; }
    }
}
