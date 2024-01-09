namespace MyWebApp.Models
{
    public class InspectionListItemForViewModel
    {
        public InspectionPreviewModel Inspection {  get; set; }
        public List<InspectionListItemForViewModel> InnerInspections { get; set; }

        public InspectionListItemForViewModel(InspectionPreviewModel model)
        {
            InnerInspections = new List<InspectionListItemForViewModel>();
            this.Inspection = model;
        }
    }
}
