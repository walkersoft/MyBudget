namespace MyBudget.Application.Entities
{
    public class ExpenseCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Expense> Expenses { get; private set; } = new();
    }
}
