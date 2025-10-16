using FamilyBudgetTracker.Data;
using FamilyBudgetTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FamilyBudgetTracker.Services;

/// <summary>
/// ExpenseService: Handles CRUD operations and reporting for expenses.
/// Provides methods that use the authenticated HttpContext user as well as explicit userId-based methods
/// (useful for demo data and programmatic access).
/// </summary>
public class ExpenseService
{
	private readonly ApplicationDbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ILogger<ExpenseService> _logger;

	public ExpenseService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ExpenseService> logger)
	{
		_context = context;
		_httpContextAccessor = httpContextAccessor;
		_logger = logger;
	}

	/// <summary>
	/// Gets the current authenticated user's ID. Throws if not authenticated.
	/// </summary>
	private string GetUserId()
	{
		var user = _httpContextAccessor.HttpContext?.User;
		var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
		if (string.IsNullOrEmpty(userId))
		{
			_logger.LogWarning("No authenticated user found in HttpContext.");
			throw new InvalidOperationException("User not authenticated. Please log in.");
		}
		return userId;
	}

	// --- Auth-based methods (use current authenticated user) ---
	public async Task<List<Expense>> GetExpensesAsync(string searchCategory, int? searchYear)
	{
		var userId = GetUserId();
		return await GetExpensesForUserAsync(userId, searchCategory, searchYear);
	}

	public async Task<Expense?> GetExpenseByIdAsync(int id)
	{
		var userId = GetUserId();
		return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
	}

	public async Task AddExpenseAsync(Expense expense)
	{
		var userId = GetUserId();
		await AddExpenseForUserAsync(userId, expense);
	}

	public async Task UpdateExpenseAsync(Expense expense)
	{
		var userId = GetUserId();
		await UpdateExpenseForUserAsync(userId, expense);
	}

	public async Task DeleteExpenseAsync(int id)
	{
		var userId = GetUserId();
		await DeleteExpenseForUserAsync(userId, id);
	}

	public async Task<Dictionary<string, decimal>> GetExpenseReportAsync(int year, int month)
	{
		var userId = GetUserId();
		return await GetExpenseReportForUserAsync(userId, year, month);
	}

	// --- UserId-based methods (explicit user id) ---
	public async Task<List<Expense>> GetExpensesForUserAsync(string userId, string? searchCategory, int? searchYear)
	{
		try
		{
			var query = _context.Expenses.Where(e => e.UserId == userId);
			if (!string.IsNullOrEmpty(searchCategory)) query = query.Where(e => e.Category.Contains(searchCategory));
			if (searchYear.HasValue) query = query.Where(e => e.Date.Year == searchYear.Value);
			return await query.OrderByDescending(e => e.Date).ToListAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error retrieving expenses for user {UserId}", userId);
			throw;
		}
	}

	public async Task AddExpenseForUserAsync(string userId, Expense expense)
	{
		try
		{
			expense.UserId = userId;
			_context.Expenses.Add(expense);
			await _context.SaveChangesAsync();
			_logger.LogInformation("Added expense {ExpenseId} for user {UserId}", expense.Id, userId);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error adding expense for user {UserId}", userId);
			throw;
		}
	}

	public async Task UpdateExpenseForUserAsync(string userId, Expense expense)
	{
		try
		{
			var existing = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == expense.Id && e.UserId == userId);
			if (existing != null)
			{
				_context.Entry(existing).CurrentValues.SetValues(expense);
				await _context.SaveChangesAsync();
				_logger.LogInformation("Updated expense {ExpenseId} for user {UserId}", expense.Id, userId);
			}
			else
			{
				_logger.LogWarning("No expense {ExpenseId} found for user {UserId} while updating.", expense.Id, userId);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error updating expense {ExpenseId} for user {UserId}", expense.Id, userId);
			throw;
		}
	}

	public async Task DeleteExpenseForUserAsync(string userId, int id)
	{
		try
		{
			var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
			if (expense != null)
			{
				_context.Expenses.Remove(expense);
				await _context.SaveChangesAsync();
				_logger.LogInformation("Deleted expense {ExpenseId} for user {UserId}", id, userId);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error deleting expense {ExpenseId} for user {UserId}", id, userId);
			throw;
		}
	}

	public async Task<Dictionary<string, decimal>> GetExpenseReportForUserAsync(string userId, int year, int month)
	{
		try
		{
			var reportData = await _context.Expenses
				.Where(e => e.UserId == userId && e.Date.Year == year && e.Date.Month == month)
				.GroupBy(e => e.Category)
				.Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
				.ToDictionaryAsync(r => r.Category, r => r.Total);
			return reportData;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error generating expense report for user {UserId} {Year}-{Month}.", userId, year, month);
			throw;
		}
	}

	/// <summary>
	/// Seed the database with demo expenses for a test user (for troubleshooting/demo only).
	/// </summary>
	public async Task SeedDemoExpensesAsync()
	{
		try
		{
			var demoUserId = "demo-user-0001";
			if (!await _context.Expenses.AnyAsync(e => e.UserId == demoUserId))
			{
				_context.Expenses.AddRange(new[] {
					new Expense { Amount = 50, Category = "Food", Date = DateTime.Now.AddDays(-2), UserId = demoUserId },
					new Expense { Amount = 20, Category = "Transport", Date = DateTime.Now.AddDays(-1), UserId = demoUserId },
					new Expense { Amount = 100, Category = "Rent", Date = DateTime.Now.AddDays(-10), UserId = demoUserId },
					new Expense { Amount = 15, Category = "Utilities", Date = DateTime.Now.AddDays(-5), UserId = demoUserId }
				});
				await _context.SaveChangesAsync();
				_logger.LogInformation("Seeded demo expenses for user {UserId}", demoUserId);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error seeding demo expenses.");
			throw;
		}
	}
}
