using JobTrackr.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTrackr.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Email).HasMaxLength(256).IsRequired();
            e.Property(u => u.FullName).HasMaxLength(256).IsRequired();
        });

        // JobApplication
        modelBuilder.Entity<JobApplication>(e =>
        {
            e.HasKey(j => j.Id);
            e.Property(j => j.CompanyName).HasMaxLength(256).IsRequired();
            e.Property(j => j.RoleTitle).HasMaxLength(256).IsRequired();
            e.Property(j => j.Country).HasMaxLength(100).IsRequired();
            e.Property(j => j.JobUrl).HasMaxLength(1000);
            e.Property(j => j.Status).HasConversion<string>();
            e.Property(j => j.Source).HasConversion<string>();

            e.HasOne(j => j.User)
             .WithMany(u => u.JobApplications)
             .HasForeignKey(j => j.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
