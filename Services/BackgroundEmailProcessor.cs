using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class BackgroundEmailProcessor : BackgroundService
{
    private readonly ILogger<BackgroundEmailProcessor> _logger;
    private readonly EmailProcessor _emailProcessor;

    public BackgroundEmailProcessor(ILogger<BackgroundEmailProcessor> logger, EmailProcessor emailProcessor)
    {
        _logger = logger;
        _emailProcessor = emailProcessor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Email Processor is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Processing emails at: {time}", DateTimeOffset.Now);

            try
            {
                var consolidatedEmails = await _emailProcessor.ProcessEmailsAsync();
                // Handle the consolidated emails (e.g., save them to the database, send notifications, etc.)
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing emails.");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Adjust the interval as needed
        }

        _logger.LogInformation("Background Email Processor is stopping.");
    }
}