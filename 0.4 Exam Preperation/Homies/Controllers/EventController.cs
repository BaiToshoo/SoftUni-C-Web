using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace Homies.Controllers;
[Authorize]
public class EventController : Controller
{
    private readonly HomiesDbContext data;

    public EventController(HomiesDbContext context)
    {
        data = context;
    }
    public async Task<IActionResult> All()
    {
        var events = await data.Events
            .AsNoTracking()
            .Select(e => new EventInfoViewModel(
                e.Id,
                e.Name,
                e.Start,
                e.Type.Name,
                e.Organiser.UserName))
            .ToListAsync();

        return View(events);
    }

    //public async Task<IActionResult> Join(int id)
    //{
    //    var e = await data.Events
    //        .Where(e => e.Id == id)
    //        .Include(e => e.EventsParticipants)
    //        .FirstOrDefaultAsync();

    //    if (e == null)
    //    {
    //        return BadRequest();
    //    }

    //    string userId = GetUserId();

    //    if (e.EventsParticipants.Any(ep => ep.HelperId == userId))
    //    {
    //        return RedirectToAction("Joined");
    //    }

    //}

    private string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }
}
