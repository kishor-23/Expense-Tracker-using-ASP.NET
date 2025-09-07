using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expense_tracker_web_app.Models;

public class Expense
{
    [Key] // Primary key [Key] // Primary key
    public int Id { get; set; }
    [Required] // Non-nullable column
    [MaxLength(100)] // Maximum length of 100 characters
    public string Name { get; set; }

    [Column(TypeName = "decimal(18,2)")] // Custom column type
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    // Foreign key for Category
     [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
public Category? Category { get; set; }
}