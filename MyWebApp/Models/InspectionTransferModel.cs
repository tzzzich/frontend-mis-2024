namespace MyWebApp.Models
{
    public class InspectionTransferModel
    {
        public Guid? PatientId { get; set; }
        public bool? IsSecondary { get; set; }
        public Guid? ParentId { get; set; }
    }
}
