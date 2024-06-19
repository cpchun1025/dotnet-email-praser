using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<EmailService>(provider =>
{
    var configuration = builder.Configuration;
    return new EmailService(
        configuration["Graph:ClientId"],
        configuration["Graph:TenantId"],
        configuration["Graph:ClientSecret"]
    );
});

builder.Services.AddSingleton<TemplateMatcher>(provider =>
{
    var matcher = new TemplateMatcher();
    matcher.AddTemplate(new Regex(@"HSBC.*Credit Card Transaction.*Notification", RegexOptions.IgnoreCase), new Template1());
    matcher.AddTemplate(new Regex(@"HSBC.*Personal Credit Card.*eAlert", RegexOptions.IgnoreCase), new Template2());
    // Add more templates as needed
    return matcher;
});

builder.Services.AddTransient<EmailProcessor>();

var app = builder.Build();

// Define minimal API endpoints
app.MapGet("/process-emails", async (EmailProcessor emailProcessor) =>
{
    var processedEmails = await emailProcessor.ProcessEmailsAsync();
    return Results.Ok(processedEmails);
});

app.MapControllers();

app.Run();