namespace MyBudget.Application.Entities
{
    public class Expense : BaseEntity
    {
        public ExpenseType ExpenseType { get; set; }
        public string Source { get; set; } = string.Empty;
        public decimal? Amount { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public Guid? CategoryId { get; set; }
    }

    public enum ExpenseType
    {
        Variable = 1,
        Stable = 2,
        Fixed = 3
    }
}
