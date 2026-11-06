using Microsoft.EntityFrameworkCore;
using VacancySite.Models;

namespace VacancySite.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Application> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Application>()
            .HasOne<Vacancy>()
            .WithMany(v => v.Applications)
            .HasForeignKey(a => a.AppliedVacancyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}