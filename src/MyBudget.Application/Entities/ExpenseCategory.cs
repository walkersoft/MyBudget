using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Application.Entities
{
    public class ExpenseCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<BaseExpense> Expenses { get; private set; } = new();
    }
}
