using MyBudget.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Application.Common
{
    public interface IAppDataContext
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
    }
}
