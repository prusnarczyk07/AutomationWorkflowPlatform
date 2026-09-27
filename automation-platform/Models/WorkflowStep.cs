namespace automation_platform.Models
{
    public class WorkflowStep
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
    }
}
