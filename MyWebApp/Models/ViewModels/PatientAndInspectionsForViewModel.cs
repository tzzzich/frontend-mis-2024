namespace MyWebApp.Models.ViewModels
{
    public class PatientAndInspectionsForViewModel
    {
        public PatientModel Patient { get; set; }

        public InspectionListForViewModel Inspections { get; set; }

        public InspectionsFilter Filter { get; set; }
        public List<Icd10RecordModel> Icd10RootsList { get; set; }

    }
}
