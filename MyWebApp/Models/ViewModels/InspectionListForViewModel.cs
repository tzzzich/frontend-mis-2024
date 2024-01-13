namespace MyWebApp.Models.ViewModels
{
    public class InspectionListForViewModel
    {
        public PageInfoModel Pagination { get; set; }

        public List<InspectionListItemForViewModel> Inspections { get; set; }

        public bool IsDead { get; set; }

        public InspectionListForViewModel(InspectionPagedListModel model, bool grouped=false)
        {
            this.Pagination = model.Pagination;
            this.Inspections = new List<InspectionListItemForViewModel>();
            IsDead = false;
            foreach(var item in model.Inspections)
            {
                if (item.Conclusion == "Death") IsDead = true;
                if (!grouped)
                {
                    this.Inspections.Add(new InspectionListItemForViewModel(item));
                }
                else
                {
                    if (item.PreviousId == null || item.PreviousId.GetValueOrDefault().ToString() == "")
                    {
                        this.Inspections.Add(new InspectionListItemForViewModel(item));
                    }
                }
            }
        }
    }
}
