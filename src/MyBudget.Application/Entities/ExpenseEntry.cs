using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Application.Entities
{
    public class ExpenseEntry : BaseEntity
    {
        public DateOnly CreationDate { get; private set; }
        public BaseExpense Expense { get; private set; }
        public decimal Amount { get; private set; }
        
        public ExpenseEntry(DateOnly creationDate, BaseExpense expense, decimal amount)
        {
            CreationDate = creationDate;
            Expense = expense;
            Amount = amount;
        }
    }
}
