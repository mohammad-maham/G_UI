using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models.ComponentModels
{
    public class BaseModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}