public class SimpleTemplate : EmailTemplate
{
    public override string Process(string body)
    {
        // Implement simple processing logic
        return $"Processed by SimpleTemplate: {body}";
    }
}