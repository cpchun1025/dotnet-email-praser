using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmailService
{
    private readonly ApplicationDbContext _context;

    public EmailService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetEmailsAsync()
    {
        return await _context.Messages.ToListAsync();
    }
}