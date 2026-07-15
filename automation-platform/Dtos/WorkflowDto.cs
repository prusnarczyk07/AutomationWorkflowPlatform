namespace automation_platform.Dtos
{
    public class WorkflowDto
    {
        public string Trigger { get; set; } = string.Empty;
        public List<string> Steps { get; set; } = new();
    }
}
