using KartoshkaEvent.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace KartoshkaEvent.Api.Controllers
{
    [Route("/api/[controller]")]
    public class TetsEfController(
        IKartoshkaEventContext context) : ControllerBase
    {
        [HttpGet("test1")]
        public async Task Test1()
        {
            var events1 = await context
                .Events
                .Include(e => e.Images)
                .ToListAsync();

            var enumerator = events1.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var enumeratorImage = enumerator.Current.Images.GetEnumerator();

                while (enumeratorImage.MoveNext())
                {
                    Console.WriteLine(enumeratorImage.Current.ImagePath);
                }
            }

            var events = await context
                .Events
                .Include(e => e.Images)
                .AsSplitQuery()
                .ToListAsync();
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var a = context.Events.Where(a => a.Subject == "Neame").Where(a => a.Description == "sdasdasd").ToList();

            return Ok();
        }
    }
}
