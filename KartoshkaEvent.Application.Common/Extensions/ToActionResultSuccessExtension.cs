using KartoshkaEvent.Domain.Common.Extensions;
using KartoshkaEvent.Domain.Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Application.Common.Extensions
{
    public static class ToActionResultSuccessExtension
    {
        public static IActionResult ToActionResult(this Success success)
            => new StatusCodeResult(success.StatusCode.GetInt());
    }
}
