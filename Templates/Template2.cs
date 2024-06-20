public class Template2 : IEmailTemplate
{
    public ConsolidatedEmail ParseEmail(Message email)
    {
        // Parse email content specific to Template2
        return new ConsolidatedEmail
        {
            Sender = email.Sender,
            Subject = email.Subject,
            // Populate other fields as needed
        };
    }
}