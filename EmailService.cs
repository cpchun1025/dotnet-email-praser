using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EmailService
{
    private readonly GraphServiceClient _graphClient;

    public EmailService(string clientId, string tenantId, string clientSecret)
    {
        var confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(clientId)
            .WithTenantId(tenantId)
            .WithClientSecret(clientSecret)
            .Build();

        var authProvider = new ClientCredentialProvider(confidentialClientApplication);
        _graphClient = new GraphServiceClient(authProvider);
    }

    public async Task<List<Message>> GetEmailsAsync()
    {
        var messages = await _graphClient.Me.Messages
            .Request()
            .GetAsync();

        return messages.CurrentPage.ToList();
    }
}
