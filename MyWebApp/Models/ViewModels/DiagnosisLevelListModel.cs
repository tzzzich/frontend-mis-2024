namespace MyWebApp.Models.ViewModels
{
    public class DiagnosisLevelListModel
    {
        public int Position { get; set; }
        public int Level { get; set; }
        public IEnumerable<Icd10RecordModel> Diagnoses { get; set; } = new List<Icd10RecordModel>();
    }
}
