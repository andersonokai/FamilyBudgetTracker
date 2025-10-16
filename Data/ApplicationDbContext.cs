using FamilyBudgetTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudgetTracker.Data;

/// <summary>
/// Application database context. Inherits from IdentityDbContext to include Identity tables.
/// Contains the Expenses DbSet used throughout the application.
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    /// <summary>
    /// Expenses table storing user expense entries.
    /// </summary>
    public DbSet<Expense> Expenses { get; set; }
}
