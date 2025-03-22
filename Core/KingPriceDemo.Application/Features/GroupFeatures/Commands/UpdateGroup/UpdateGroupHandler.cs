using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.UpdateGroup
{
    public class UpdateGroupHandler(IGroupQueryRepository queryRepo, IGroupCommandRepository commandRepo)
        : ICommandHandler<UpdateGroupRequest>
    {
        public async Task<Unit> Handle(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            var group = await queryRepo.Groups
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group is null)
                throw new NotFoundException(nameof(Group), request.Id);

            group.Name = request.Name;

            await commandRepo.UpdateAsync(group, true, cancellationToken);

            return Unit.Value;
        }
    }
}
