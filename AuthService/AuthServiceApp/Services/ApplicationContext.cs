using AuthServiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApp.Services;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationContext()
    {
        // var sql = Database.GenerateCreateScript();
        // Console.WriteLine(sql);
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=storage;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasAlternateKey(u => u.Login);
    }
}