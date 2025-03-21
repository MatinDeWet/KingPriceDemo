namespace KingPriceDemo.Application.Features.UserFeatures.Commands.UpdateUser
{
    public record UpdateUserRequest(string FullName, string Surname, string Email, string CellphoneNumber) : ICommand;
}
