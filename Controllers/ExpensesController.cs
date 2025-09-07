using System.Globalization;
using expense_tracker_web_app.Data;
using expense_tracker_web_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker_web_app.Controllers;

public class ExpensesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Expenses
    public async Task<IActionResult> Index()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        var expenses = await _context.Expenses
            .Include(e => e.Category)
            .ToListAsync();
        return View(expenses);
    }

    // POST: Expenses/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id,Name,Amount,Date,CategoryId")] Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Expense created successfully!" });
    }

    // POST: Expenses/Edit
    [HttpPost]
    public async Task<IActionResult> Edit([Bind("Id,Name,Amount,Date,CategoryId")] Expense expense)
    {
        if (expense == null)
            return Json(new { success = false, message = "Invalid data." });

        var existing = await _context.Expenses.FindAsync(expense.Id);
        if (existing == null)
            return Json(new { success = false, message = "Expense not found." });

     
        try
        {
            // Update only fields from form
            existing.Name = expense.Name;
            existing.Amount = expense.Amount;
            existing.Date = expense.Date;
            existing.CategoryId = expense.CategoryId;

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Expense updated successfully!" });
        }
        catch (DbUpdateConcurrencyException)
        {
            return Json(new { success = false, message = "Concurrency error occurred." });
        }
    }

    // POST: Expenses/Delete/{id}
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
            return Json(new { success = false, message = "Expense not found." });

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Expense deleted successfully!" });
    }

    // GET: Expenses/GetExpenseChartData
    [HttpGet]
    public IActionResult GetExpenseChartData()
    {
        var data = _context.Expenses
            .Include(e => e.Category)
            .GroupBy(e => e.Category != null ? e.Category.Name : "Uncategorized")
            .Select(g => new
            {
                category = g.Key,
                total = g.Sum(e => e.Amount)
            })
            .ToList();

        return Json(data);
    }

    [HttpGet]
public JsonResult GetMonthlySummary()
{
    var summary = _context.Expenses
        .GroupBy(e => new { e.Date.Year, e.Date.Month })
        .Select(g => new {
            month = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month)} {g.Key.Year}",
            total = g.Sum(e => e.Amount)
        })
        .OrderByDescending(g => g.month)
        .ToList();

    return Json(summary);
}

}
