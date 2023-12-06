namespace MyBudget.Application.Entities
{
    public class ExpenseEntry : BaseEntity
    {
        public Expense Expense { get; set; }
        public DateOnly CreationDate { get; set; }
        public decimal Amount { get; set; }
    }
}
