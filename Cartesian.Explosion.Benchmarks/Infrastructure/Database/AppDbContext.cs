using Cartesian.Explosion.Benchmarks.Domain.Entities;
using Cartesian.Explosion.Benchmarks.Presentation.Benchmarks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cartesian.Explosion.Benchmarks.Infrastructure.Database;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=cartesian.db"); 
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(builder =>
        {
            builder.HasMany(s => s.Grades)
            .WithOne()
            .HasForeignKey("StudentId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Homeworks)
            .WithOne()
            .HasForeignKey("StudentId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Grade>(builder =>
        {
            builder.Property<int>("StudentId");
            builder.HasIndex("StudentId");
        });

        modelBuilder.Entity<Homework>(builder =>
        {
            builder.Property<int>("StudentId");
            builder.HasIndex("StudentId");
        });

    }
}
