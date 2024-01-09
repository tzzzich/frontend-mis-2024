using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Icd10RecordModel
    {
        [Required]
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
