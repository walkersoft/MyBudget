namespace MyBudget.Application.Entities
{
    public class BaseExpense : BaseEntity
    {
        public string Source { get; set; } = string.Empty;
        public DateOnly EffectiveDate { get; set; }
    }

    public class VariableExpense : BaseExpense
    {
        public bool IsActive { get; set; }
    }

    public class StableExpense : BaseExpense
    {
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }

    public class FixedExpense : BaseExpense
    {
        public decimal Amount { get; set; }
        public DateOnly ExpirationDate { get; set; }
    }
}
