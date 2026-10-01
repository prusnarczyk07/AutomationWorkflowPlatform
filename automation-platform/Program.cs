using automation_platform.Data;
using automation_platform.Repositories;
using automation_platform.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IDiscordService, DiscordService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();
builder.Services.AddScoped<IWorkflowStepHandler, DiscordStepHandler>();
builder.Services.AddScoped<IWorkflowStepHandler, LogStepHandler>();
builder.Services.AddScoped<IWorkflowStepHandler, HttpStepHandler>();
builder.Services.AddScoped<IWorkflowRepository, EfWorkflowRepository>();

// InMemory repository - used for testing without database
// builder.Services.AddSingleton<IWorkflowRepository, InMemoryWorkflowRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddHttpClient();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
