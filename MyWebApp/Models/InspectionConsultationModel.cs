using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionConsultationModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("inspectionId")]
        public string InspectionId { get; set; }

        [JsonProperty("speciality")]
        public SpecialityModel Speciality { get; set; }

        [JsonProperty("rootComment")]
        [Required(ErrorMessage = "Поле 'rootComment' является обязательным.")]
        public InspectionCommentModel RootComment { get; set; }

        [JsonProperty("commentsNumber")]
        [Required(ErrorMessage = "Поле 'commentsNumber' является обязательным.")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество комментариев должно быть положительным.")]
        public int CommentsNumber { get; set; }
    }
}
