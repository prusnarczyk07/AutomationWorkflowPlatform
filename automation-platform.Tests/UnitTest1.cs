using automation_platform.Dtos;
using automation_platform.Repositories;
using automation_platform.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace automation_platform.Tests;

public class WorkflowServiceTests
{
    [Fact]
    public async Task CreateWorkflow_ShouldCreateWorkflow_WhenDataIsValid()
    {
        // Arrange
        var repository = new Mock<IWorkflowRepository>();
        var logger = new Mock<ILogger<WorkflowService>>();

        var handlers = new List<IWorkflowStepHandler>();

        var service = new WorkflowService(
            handlers,
            repository.Object,
            logger.Object);

        var dto = new WorkflowDto
        {
            Name = "Test workflow",
            Trigger = "test",
            Steps = new List<WorkflowStepDto>
            {
                new WorkflowStepDto
                {
                    Type = "http",
                    Url = "https://example.com",
                    Method = "GET"
                }
            }
        };

        // Act
        var result = await service.CreateWorkflow(dto);

        // Assert
        Assert.NotNull(result.workflow);
        Assert.Null(result.error);
        Assert.Equal("Test workflow", result.workflow.Name);
        Assert.Single(result.workflow.Steps);

        repository.Verify(
            x => x.Add(It.IsAny<automation_platform.Models.Workflow>()),
            Times.Once);
    }
}