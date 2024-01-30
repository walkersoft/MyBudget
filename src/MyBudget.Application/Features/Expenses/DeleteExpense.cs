using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Application.Features.Expenses
{
    internal readonly record struct DeleteExpense(Guid Id);
}
