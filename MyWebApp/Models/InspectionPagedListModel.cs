﻿namespace MyWebApp.Models
{
    public class InspectionPagedListModel
    {
        public List<InspectionPreviewModel> Inspections { get; set; }
        public PageInfoModel Pagination { get; set; }
    }
}
