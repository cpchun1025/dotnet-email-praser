using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton<TemplateMatcher>(provider =>
        {
            var matcher = new TemplateMatcher();
            matcher.AddTemplate(new Regex(@"HSBC.*Credit Card Transaction.*Notification", RegexOptions.IgnoreCase), new Template1());
            matcher.AddTemplate(new Regex(@"HSBC.*Personal Credit Card.*eAlert", RegexOptions.IgnoreCase), new Template2());
            matcher.AddTemplate(new Regex(@"^(FW:|RE:)\s+.*COMPANYNAME(?:\s+WITH)?(?:\s+CODE)?\s*:\s*(?:[A-Z]{3})?(?:[A-Z]{3})?\s*-\s*(?:USERNAME)?(?:\s+PRODUCT)?(?:\s+ANYTHING)?$", RegexOptions.IgnoreCase), new ComplexTemplate());
            // Add more templates as needed
            return matcher;
        });

        services.AddTransient<EmailService>();
        services.AddTransient<EmailProcessor>();
        services.AddHostedService<BackgroundEmailProcessor>();
    })
    .Build();

await host.RunAsync();