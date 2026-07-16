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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Values>(entity =>
        {
            entity.ToTable("values");

            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");
            entity.Property(e => e.Value).HasColumnName("value");
        });
        
        modelBuilder.Entity<Results>(entity =>
        {
            entity.ToTable("results");

            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.AvgExecutionTime).HasColumnName("avg_execution_time");
            entity.Property(e => e.AvgValue).HasColumnName("avg_value");
            entity.Property(e => e.DeltaSeconds).HasColumnName("delta_seconds");
            entity.Property(e => e.MaxValue).HasColumnName("max_value");
            entity.Property(e => e.MedianValue).HasColumnName("median_value");
            entity.Property(e => e.MinValue).HasColumnName("min_value");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });
    }
}