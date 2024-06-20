public interface IEmailTemplate
{
    ConsolidatedEmail ParseEmail(Message email);
}