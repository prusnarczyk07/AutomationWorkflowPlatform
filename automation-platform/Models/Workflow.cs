namespace automation_platform.Models
{
    public class Workflow
    {
        public string Trigger { get; set; } = string.Empty;
        public List<string> Steps { get; set; } = new();
    }
}
