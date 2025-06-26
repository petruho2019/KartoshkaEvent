using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Features.Commands.Users.Registration;
using KartoshkaEvent.Application.Features.Queries.Users.Login;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Services.Validations
{
    public class UserValidationService(IKartoshkaEventContext context) : IUserValidationService
    {
        public async Task<Result<User>> ValidateLoginAsync(LoginQuery query)
        {
            var userFromDb = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(query.Email));

            if (userFromDb == null)
                return Result<User>.BadRequest("User not found");
            if (!BCrypt.Net.BCrypt.Verify(query.Password, userFromDb.Password))
                return Result<User>.BadRequest("Incorrect password");

            return Result<User>.Ok(userFromDb);
        }

        public async Task<Result> ValidateRegistrationAsync(RegistrationCommand command)
        {
            var userFromDb = await context.Users.ToListAsync();

            foreach (var user in userFromDb)
            {
                if (user.Email.Equals(command.Email))
                    return Result.BadRequest("Email is busy");
                if (user.NickName.Equals(command.NickName))
                    return Result.BadRequest("NickName is busy");
            }

            return Result.NoContent();
        }
    }
}
