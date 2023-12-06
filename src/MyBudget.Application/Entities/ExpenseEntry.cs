namespace MyBudget.Application.Entities
{
    public class ExpenseEntry : BaseEntity
    {
        public DateOnly CreationDate { get; private set; }
        public Expense Expense { get; private set; }
        public decimal Amount { get; private set; }
        
        public ExpenseEntry(DateOnly creationDate, Expense expense, decimal amount)
        {
            CreationDate = creationDate;
            Expense = expense;
            Amount = amount;
        }
    }
}
