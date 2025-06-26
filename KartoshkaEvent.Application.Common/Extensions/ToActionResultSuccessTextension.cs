using KartoshkaEvent.Domain.Common.Extensions;
using KartoshkaEvent.Domain.Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Application.Common.Extensions
{
    public static class ToActionResultSuccessTExtension
    {
        public static IActionResult ToActionResult<T>(this Success<T> success)
            => new ObjectResult(success.Data) { StatusCode = success.StatusCode.GetInt() };
    }
}
