﻿using AirlineWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineWeb.Data;

public class AirlineDbContext : DbContext
{
    public AirlineDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; } = default!;
}