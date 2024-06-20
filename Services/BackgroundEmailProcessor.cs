using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ML;

public class BackgroundEmailProcessor : BackgroundService
{
    private readonly ILogger<BackgroundEmailProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly PredictionEngine<EmailSubjectData, EmailSubjectPrediction> _predictionEngine;
    private readonly TemplateMatcher _templateMatcher;

    public BackgroundEmailProcessor(
        ILogger<BackgroundEmailProcessor> logger,
        IServiceProvider serviceProvider,
        PredictionEngine<EmailSubjectData, EmailSubjectPrediction> predictionEngine,
        TemplateMatcher templateMatcher)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _predictionEngine = predictionEngine;
        _templateMatcher = templateMatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Email Processor is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var pendingEmails = dbContext.RawEmailData.Where(e => !e.IsProcessed).ToList();

                foreach (var email in pendingEmails)
                {
                    var emailSubjectData = new EmailSubjectData { Subject = email.Subject };
                    var prediction = _predictionEngine.Predict(emailSubjectData);

                    var matchedTemplate = _templateMatcher.Match(prediction.PredictedLabel);

                    var consolidatedEmail = new ConsolidatedEmail
                    {
                        RawEmailDataId = email.Id,
                        ProcessedTemplate = matchedTemplate.Process(email.Body)
                    };

                    dbContext.ConsolidatedEmails.Add(consolidatedEmail);
                    email.IsProcessed = true;
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);  // Adjust the interval as needed
        }

        _logger.LogInformation("Background Email Processor is stopping.");
    }
}