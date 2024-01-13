namespace MyWebApp.Models.ViewModels
{
    public class RenderInspectionModel
    {
        public InspectionListItemForViewModel Inspection {  get; set; }

        public int Level { get; set; }

        public bool? isChainElement { get; set; }

        public Guid? ParentId { get; set; }
    }
}
