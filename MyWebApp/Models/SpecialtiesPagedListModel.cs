namespace MyWebApp.Models
{
    public class SpecialtiesPagedListModel
    {
        public List<SpecialityModel> Specialties { get; set; }

        public PageInfoModel Pagination { get; set; }
    }
}
