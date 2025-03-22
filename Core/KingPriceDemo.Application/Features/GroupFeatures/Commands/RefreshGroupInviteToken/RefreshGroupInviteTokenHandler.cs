using KingPriceDemo.Application.Common.Extensions;
using KingPriceDemo.Application.Repositories.CommandRepositories;
using KingPriceDemo.Application.Repositories.QueryRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.RefreshGroupInviteToken
{
    public class RefreshGroupInviteTokenHandler(IGroupQueryRepository queryRepo, IGroupCommandRepository commandRepo)
        : ICommandHandler<RefreshGroupInviteTokenRequest>
    {
        public async Task<Unit> Handle(RefreshGroupInviteTokenRequest request, CancellationToken cancellationToken)
        {
            var group = await queryRepo.Groups
                .Where(g => g.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group is null)
                throw new NotFoundException(nameof(Group), request.Id);

            group.InviteToken = StringTools.GenerateRandomString(20);

            await commandRepo.UpdateAsync(group, true, cancellationToken);

            return Unit.Value;
        }
    }
}
