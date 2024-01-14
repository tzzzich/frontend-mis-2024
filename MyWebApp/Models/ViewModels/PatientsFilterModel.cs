namespace MyWebApp.Models.ViewModels
{
    public class PatientsFilterModel
    {
        public string? Name { get; set; }

        public string[]? Conclusions { get; set; }

        public string? Sorting { get; set; }

        public bool? ScheduledVisits { get; set; }

        public bool? OnlyMine { get; set; }

        public string? Page { get; set; }

        public string? Size { get; set; }
    }
}
