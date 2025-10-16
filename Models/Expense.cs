using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyBudgetTracker.Models
{
    public class Expense
    {
        /// <summary>
        /// Primary key for the Expense entity.
        /// </summary>
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

    /// <summary>
    /// Category for the expense (e.g. Food, Transport).
    /// </summary>

        [Required]
        public required string Category { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// The Id of the user who created the expense. This links to the Identity user.
        /// </summary>
        public string? UserId { get; set; }
    }
}
