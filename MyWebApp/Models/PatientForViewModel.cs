using MyWebApp.Models.ViewModels;

namespace MyWebApp.Models
{
    public class PatientForViewModel
    {
        public PatientModel Patient { get; set; }

        public InspectionListForViewModel Inspections { get; set; }

        public PatientInspectionsFilter Filter { get; set; }
        public List<Icd10RecordModel> Icd10RootsList { get; set; }

    }
}
