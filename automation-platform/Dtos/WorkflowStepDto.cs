using System.ComponentModel.DataAnnotations;

namespace automation_platform.Dtos
{
    public class WorkflowStepDto
    {
        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Url { get; set; } = string.Empty;

        [Required]
        public string Method { get; set; } = string.Empty;
    }
}
