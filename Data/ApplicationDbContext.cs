using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Message> Messages { get; set; }
    public DbSet<RawEmailData> RawEmailData { get; set; }
    public DbSet<ConsolidatedEmail> ConsolidatedEmails { get; set; }    
}