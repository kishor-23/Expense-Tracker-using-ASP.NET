namespace expense_tracker_web_app.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property for related expenses
    public ICollection<Expense> Expenses { get; set; }
}