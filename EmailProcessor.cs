using System.Collections.Generic;
using System.Threading.Tasks;

public class EmailProcessor
{
    private readonly EmailService _emailService;
    private readonly TemplateMatcher _templateMatcher;

    public EmailProcessor(EmailService emailService, TemplateMatcher templateMatcher)
    {
        _emailService = emailService;
        _templateMatcher = templateMatcher;
    }

    public async Task<List<ConsolidatedEmail>> ProcessEmailsAsync()
    {
        var emails = await _emailService.GetEmailsAsync();
        var consolidatedEmails = new List<ConsolidatedEmail>();

        foreach (var email in emails)
        {
            var template = _templateMatcher.MatchTemplate(email.Subject);
            if (template != null)
            {
                var consolidatedEmail = template.ParseEmail(email);
                consolidatedEmails.Add(consolidatedEmail);
            }
            else
            {
                // Handle emails that don't match any template
            }
        }

        return consolidatedEmails;
    }
}