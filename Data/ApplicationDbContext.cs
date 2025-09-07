using Microsoft.EntityFrameworkCore;
using expense_tracker_web_app.Models;

namespace expense_tracker_web_app.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Food" },
            new Category { Id = 2, Name = "Transport" },
            new Category { Id = 3, Name = "Entertainment" }
        );

        // Seed data for Expenses
        modelBuilder.Entity<Expense>().HasData(
            new Expense { Id = 1, Name = "Groceries", Amount = 50.75m, Date = DateTime.Now.AddDays(-5), CategoryId = 1 },
            new Expense { Id = 2, Name = "Bus Ticket", Amount = 15.00m, Date = DateTime.Now.AddDays(-3), CategoryId = 2 },
            new Expense { Id = 3, Name = "Movie Night", Amount = 20.00m, Date = DateTime.Now.AddDays(-1), CategoryId = 3 }
        );
    }
}