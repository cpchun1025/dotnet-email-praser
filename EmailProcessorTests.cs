using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class EmailProcessorTests
{
    [Fact]
    public async Task ProcessEmailsAsync_ReturnsConsolidatedEmails()
    {
        // Arrange
        var mockEmailService = new Mock<EmailService>("clientId", "tenantId", "clientSecret");
        var mockTemplateMatcher = new Mock<TemplateMatcher>();

        var email = new Message { Subject = "HSBC Credit Card Transaction Notification", From = new Recipient { EmailAddress = new EmailAddress { Address = "test@hsbc.com" } } };
        var consolidatedEmail = new ConsolidatedEmail { Sender = "test@hsbc.com", Subject = "HSBC Credit Card Transaction Notification" };
        var template = new Mock<IEmailTemplate>();
        template.Setup(t => t.ParseEmail(email)).Returns(consolidatedEmail);

        mockEmailService.Setup(s => s.GetEmailsAsync()).ReturnsAsync(new List<Message> { email });
        mockTemplateMatcher.Setup(m => m.MatchTemplate(email.Subject)).Returns(template.Object);

        var emailProcessor = new EmailProcessor(mockEmailService.Object, mockTemplateMatcher.Object);

        // Act
        var result = await emailProcessor.ProcessEmailsAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("test@hsbc.com", result[0].Sender);
        Assert.Equal("HSBC Credit Card Transaction Notification", result[0].Subject);
    }

    [Fact]
    public async Task ProcessEmailsAsync_Skips