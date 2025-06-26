using KartoshkaEvent.Application.Features.Commands.Users.Registration;
using KartoshkaEvent.Application.Features.Queries.Users.Login;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Interfaces
{
    public interface IUserValidationService
    {
        Task<Result<User>> ValidateLoginAsync(LoginQuery query);
        Task<Result> ValidateRegistrationAsync(RegistrationCommand command);
    }
}
