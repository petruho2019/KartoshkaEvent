using KartoshkaEvent.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Api
{
    public class DbInitializer
    {
        public static async Task Initialize(KartoshkaEventContext context)
        {
            context.Database.EnsureCreated();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("string");

            try
            {
                await context.Database.ExecuteSqlRawAsync(@"
                INSERT INTO ""KartoshkaEvent"".""User"" (
                    ""Id"", ""Email"", ""Password"", ""Role"",
                    ""DateOfBirth"", ""PhoneNumber"", ""NickName""
                )
                SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}
                WHERE NOT EXISTS (
                    SELECT 1 FROM ""KartoshkaEvent"".""User"" WHERE ""Id"" = {0}
                );
                ",
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "petruhobusinessman@gmail.com",
                hashedPassword,
                2,
                DateTime.Parse("1990-01-01").ToUniversalTime(),
                "71234567890",
                "SuperOrganizer");

                // Запрос для VISITOR
                await context.Database.ExecuteSqlRawAsync(@"
                INSERT INTO ""KartoshkaEvent"".""User"" (
                    ""Id"", ""Email"", ""Password"", ""Role"",
                    ""DateOfBirth"", ""PhoneNumber"", ""NickName""
                )
                SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}
                WHERE NOT EXISTS (
                    SELECT 1 FROM ""KartoshkaEvent"".""User"" WHERE ""Id"" = {0}
                );
            ",
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "vladgolovko20062018@gmail.com",
                hashedPassword,
                1,
                DateTime.Parse("1990-01-01").ToUniversalTime(),
                "71234567890",
                "SuperVisitor");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
    }
}
