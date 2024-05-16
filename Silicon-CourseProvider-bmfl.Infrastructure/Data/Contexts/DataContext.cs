using Microsoft.EntityFrameworkCore;
using Silicon_CourseProvider_bmfl.Infrastructure.Data.Entities;

namespace Silicon_CourseProvider_bmfl.Infrastructure.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    public DbSet<CourseEntity> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseEntity>().ToContainer("Courses");
        modelBuilder.Entity<CourseEntity>().HasPartitionKey(x => x.Id);
        modelBuilder.Entity<CourseEntity>().OwnsMany(x => x.Authors);
        modelBuilder.Entity<CourseEntity>().OwnsOne(x => x.Content, content => content.OwnsMany(c => c.ProgramDetails));
        
    }
}
