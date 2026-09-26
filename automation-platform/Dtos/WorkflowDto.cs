using System.ComponentModel.DataAnnotations;

namespace automation_platform.Dtos
{
    public class WorkflowDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Trigger { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public List<WorkflowStepDto> Steps { get; set; } = new();
    }
}
