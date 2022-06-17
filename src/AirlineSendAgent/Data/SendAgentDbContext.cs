using AirlineSendAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineSendAgent.Data;

internal class SendAgentDbContext : DbContext
{
    public SendAgentDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; } = default!;
}
