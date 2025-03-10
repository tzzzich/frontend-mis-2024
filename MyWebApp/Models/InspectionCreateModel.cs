﻿using MyWebApp.Utils;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionCreateModel
    {
        [Required(ErrorMessage = "Поле дата не может быть пустым")]
        [DataType(DataType.DateTime)]
        [DateValidation(ErrorMessage = "Дата не может быть позже текущего времени")]
        public DateTime Date { get; set; }

        [MaxLength(5000, ErrorMessage ="Поле анамнез не может содержать более 500 символов")]
        [MinLength(1, ErrorMessage = "Поле анамнез не может быть пустым")]
        [Required(ErrorMessage = "Поле анамнез не может быть пустым")]
        public string Anamnesis { get; set; }

        [MaxLength(5000, ErrorMessage = "Поле жалобы не может содержать более 500 символов")]
        [MinLength(1, ErrorMessage = "Поле жалобы не может быть пустым")]
        [Required(ErrorMessage = "Поле жалобы не может быть пустым")]
        public string Complaints { get; set; }

        [MaxLength(5000, ErrorMessage = "Поле лечение не может содержать более 500 символов")]
        [MinLength(1, ErrorMessage = "Поле лечение не может быть пустым")]
        [Required(ErrorMessage = "Поле лечение не может быть пустым")]
        public string Treatment { get; set; }

        [Required(ErrorMessage = "Поле заключение не может быть пустым")]
        public string Conclusion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? NextVisitDate { get; set; }

        [DataType(DataType.DateTime)]
        [DateValidation(ErrorMessage = "Дата смерти не может быть позже текущего времени")]
        public DateTime? DeathDate { get; set; }

        public string? PreviousInspectionId { get; set; }

        [MinLength(1, ErrorMessage = "Должен быть как минимум один диагноз")]
        public List<DiagnosisCreateModel> Diagnoses { get; set; } = new List<DiagnosisCreateModel>();

        public List<ConsultationCreateModel> Consultations { get; set; } = new List<ConsultationCreateModel>();
    }
}
