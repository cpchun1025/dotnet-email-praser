public class Template1 : IEmailTemplate
{
    public ConsolidatedEmail ParseEmail(Message email)
    {
        // Parse email content specific to Template1
        return new ConsolidatedEmail
        {
            Sender = email.Sender,
            Subject = email.Subject,
            // Populate other fields as needed
        };
    }
}