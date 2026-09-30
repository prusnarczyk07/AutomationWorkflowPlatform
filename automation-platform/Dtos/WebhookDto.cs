using System.ComponentModel.DataAnnotations;

namespace automation_platform.Dtos
{
    public class WebhookDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Description { get; set; } = string.Empty;
    }
}
