using Microsoft.Graph;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class EmailServiceTests
{
    [Fact]
    public async Task GetEmailsAsync_ReturnsEmails()
    {
        // Arrange
        var mockGraphClient = new Mock<GraphServiceClient>();
        var mockMessagesRequest = new Mock<IMessagesCollectionRequest>();
        var mockMessagesPage = new Mock<IMessagesCollectionPage>();

        mockMessagesPage.Setup(p => p.CurrentPage).Returns(new List<Message> { new Message { Subject = "Test Email" } });
        mockMessagesRequest.Setup(r => r.GetAsync()).ReturnsAsync(mockMessagesPage.Object);
        mockGraphClient.Setup(c => c.Me.Messages.Request()).Returns(mockMessagesRequest.Object);

        var emailService = new EmailService("clientId", "tenantId", "clientSecret")
        {
            GraphClient = mockGraphClient.Object
        };

        // Act
        var emails = await emailService.GetEmailsAsync();

        // Assert
        Assert.Single(emails);
        Assert.Equal("Test Email", emails[0].Subject);
    }
}