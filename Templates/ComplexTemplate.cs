public class ComplexTemplate : IEmailTemplate
{
    public ConsolidatedEmail ParseEmail(Message email)
    {
        // Extract details from the subject using the same regex pattern
        var match = Regex.Match(email.Subject, @"^(FW:|RE:)\s+.*(COMPANYNAME(?: WITH)?(?: CODE)?\s*:\s*(?:[A-Z]{3})?(?:[A-Z]{3})?\s*-\s*(?:USERNAME)?(?: PRODUCT)?(?: ANYTHING)?)$", RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            throw new InvalidOperationException("The email subject does not match the expected pattern.");
        }

        // Extract the relevant parts from the match groups
        var companyName = match.Groups[1].Value;
        var with = match.Groups[2].Value;
        var code = match.Groups[3].Value;
        var ccy1 = match.Groups[4].Value;
        var ccy2 = match.Groups[5].Value;
        var userName = match.Groups[6].Value;
        var product = match.Groups[7].Value;
        var anything = match.Groups[8].Value;

        // Populate the ConsolidatedEmail object based on extracted values
        return new ConsolidatedEmail
        {
            Sender = email.Sender,
            Subject = email.Subject,
            // Populate other fields as needed
        };
    }
}

public class ComplexTemplate : EmailTemplate
{
    public override string Process(string body)
    {
        // Implement complex processing logic
        return $"Processed by ComplexTemplate: {body}";
    }
}