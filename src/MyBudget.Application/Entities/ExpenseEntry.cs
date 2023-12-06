namespace MyBudget.Application.Entities
{
    public class ExpenseEntry(DateOnly creationDate, Expense expense, decimal amount) : BaseEntity
    {
        public DateOnly CreationDate { get; private set; } = creationDate;
        public Expense Expense { get; private set; } = expense;
        public decimal Amount { get; private set; } = amount;
    }
}
