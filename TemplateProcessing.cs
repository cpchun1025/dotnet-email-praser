public interface IEmailTemplate
{
    ConsolidatedEmail ParseEmail(Message email);
}

public class Template1 : IEmailTemplate
{
    public ConsolidatedEmail ParseEmail(Message email)
    {
        // Parse email content specific to Template1
        return new ConsolidatedEmail
        {
            Sender = email.From.EmailAddress.Address,
            Subject = email.Subject,
            // Additional parsed fields
        };
    }
}

public class Template2 : IEmailTemplate
{
    public ConsolidatedEmail ParseEmail(Message email)
    {
        // Parse email content specific to Template2
        return new ConsolidatedEmail
        {
            Sender = email.From.EmailAddress.Address,
            Subject = email.Subject,
            // Additional parsed fields
        };
    }
}