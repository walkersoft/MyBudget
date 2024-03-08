using System;

namespace MyBudget.UI.Messages
{
    internal record ExpensesChanged;
    internal record EditExpense(Guid Id);
}
