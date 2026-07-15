namespace automation_platform.Dtos
{
    public class WorkflowDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Trigger { get; set; } = string.Empty;
        public List<string> Steps { get; set; } = new();
    }
}
