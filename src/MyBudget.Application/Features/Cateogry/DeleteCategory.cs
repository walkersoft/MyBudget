using MediatR;
using MyBudget.Application.Common;

namespace MyBudget.Application.Features.Cateogry
{
    internal readonly record struct DeleteCategory(Guid Id) : IRequest;

    internal class DeleteCategoryHandler(IAppDataContext context) : IRequestHandler<DeleteCategory>
    {
        public async Task Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            await context.DeleteCategoryAsync(request.Id);
        }
    }
}
