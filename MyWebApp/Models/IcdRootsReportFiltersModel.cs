﻿namespace MyWebApp.Models
{
    public class IcdRootsReportFiltersModel
    {
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public List<string> IcdRoots { get; set; } = new List<string>();
    }
}
