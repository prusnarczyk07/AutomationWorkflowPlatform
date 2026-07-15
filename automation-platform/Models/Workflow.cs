namespace automation_platform.Models
{
    public class Workflow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Trigger { get; set; } = string.Empty;
        public List<string> Steps { get; set; } = new();
    }
}
