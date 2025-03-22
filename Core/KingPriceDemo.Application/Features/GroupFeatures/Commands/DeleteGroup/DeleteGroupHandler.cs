using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.DeleteGroup
{
    public class DeleteGroupHandler(IGroupQueryRepository queryRepo, IGroupCommandRepository commandRepo)
        : ICommandHandler<DeleteGroupRequest>
    {
        public async Task<Unit> Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
        {
            var group = await queryRepo.Groups
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group is null)
                throw new NotFoundException(nameof(Group), request.Id);

            await commandRepo.DeleteAsync(group, true, cancellationToken);

            return Unit.Value;
        }
    }
}
