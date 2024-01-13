using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class InspectionCommentModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("content")]
        [Required(ErrorMessage = "Поле 'content' является обязательным.")]
        [StringLength(1000, ErrorMessage = "Комментарий должен содержать не более 1000 символов.")]
        public string Content { get; set; }

        [JsonProperty("author")]
        public DoctorModel Author { get; set; }

        [JsonProperty("modifyTime")]
        public DateTime? ModifyTime { get; set; }
    }
}
