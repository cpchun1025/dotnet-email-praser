using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class BackgroundEmailProcessor : BackgroundService
{
    private readonly ILogger<BackgroundEmailProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;

    public BackgroundEmailProcessor(ILogger<BackgroundEmailProcessor> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Email Processor is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Background Email Processor is processing.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var templateMatcher = scope.ServiceProvider.GetRequiredService<TemplateMatcher>();

                // Fetch raw data
                var rawEmails = dbContext.RawEmailData
                    .Where(e => e.Processed == false)
                    .ToList();

                foreach (var rawEmail in rawEmails)
                {
                    try
                    {
                        // Process data using template matcher
                        var template = templateMatcher.MatchTemplate(rawEmail.Subject);
                        if (template != null)
                        {
                            var consolidatedEmail = template.ParseEmail(rawEmail);

                            // Insert processed data into consolidated table
                            dbContext.ConsolidatedEmails.Add(consolidatedEmail);

                            // Mark raw data as processed
                            rawEmail.Processed = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing email with ID {EmailId}", rawEmail.Id);
                    }
                }

                // Save changes to the database
                await dbContext.SaveChangesAsync();
            }

            // Wait for a specified interval before processing again
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }

        _logger.LogInformation("Background Email Processor is stopping.");
    }
}