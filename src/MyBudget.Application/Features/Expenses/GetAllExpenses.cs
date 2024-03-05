using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Application.Features.Expenses
{
    internal readonly record struct GetAllExpenses : IRequest<IEnumerable<Expense>>;

    internal class GetAllExpensesHandler(IAppDataContext context) : IRequestHandler<GetAllExpenses, IEnumerable<Expense>>
    {
        public async Task<IEnumerable<Expense>> Handle(GetAllExpenses request, CancellationToken cancellationToken)
        {
            return await context.GetAllExpensesAsync();
        }
    }
}
