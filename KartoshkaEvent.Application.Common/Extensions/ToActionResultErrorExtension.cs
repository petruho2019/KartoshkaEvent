using KartoshkaEvent.Domain.Common.Extensions;
using KartoshkaEvent.Domain.Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Application.Common.Extensions
{
    public static class ToActionResultErrorExtension
    {
        public static IActionResult ToActionResult(this Error error)
            => new ObjectResult(error.ErrorMessage) { StatusCode = error.StatusCode.GetInt() };
    }
}
