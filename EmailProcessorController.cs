using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class EmailProcessorController : ControllerBase
{
    private readonly EmailProcessor _emailProcessor;

    public EmailProcessorController(EmailProcessor emailProcessor)
    {
        _emailProcessor = emailProcessor;
    }

    [HttpGet("process")]
    public async Task<IActionResult> ProcessEmails()
    {
        var processedEmails = await _emailProcessor.ProcessEmailsAsync();
        return Ok(processedEmails);
    }
}