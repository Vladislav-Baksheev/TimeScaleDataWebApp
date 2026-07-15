using Microsoft.EntityFrameworkCore;
using TimeScaleDataWebApp.Domain.Entities;

namespace TimeScaleDataWebApp.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Results> Results { get; set; }
    public DbSet<Values> Values { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}