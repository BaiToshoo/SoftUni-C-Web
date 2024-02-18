using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models.Seminar;
using SeminarHub.Models.Type;
using System.Globalization;
using System.Security.Claims;
using static SeminarHub.Data.Common.Constants;

namespace SeminarHub.Controllers;

[Authorize]
public class SeminarController : Controller
{
    private SeminarHubDbContext data;

    public SeminarController(SeminarHubDbContext context)
    {
        data = context;
    }

    [HttpGet]
    //All Seminars button page
    public async Task<IActionResult> All()
    {
        var allSeminars = await data.Seminars
            .AsNoTracking()
            .Select(s => new SeminarAllViewModel
            {
                Id = s.Id,
                Topic = s.Topic,
                Lecturer = s.Lecturer,
                Category = s.Category.Name,
                DateAndTime = s.DateAndTime.ToString(dateAndTimeFormat),
                Organizer = s.Organizer.UserName

            }).ToListAsync();

        return View(allSeminars);
    }

    [HttpGet]
    //Anounce New button page
    public async Task<IActionResult> Add()
    {
        var SeminarForm = new SeminarAddViewModel();
        SeminarForm.Categories = await GetCategories();

        return View(SeminarForm);

    }

    //Method that creates a new seminar by filling the form and pressing the Announce button
    [HttpPost]
    public async Task<IActionResult> Add(SeminarAddViewModel seminarForm)
    {
        //Validation of the model that the User has entered
        //Null check
        if (seminarForm == null)
        {
            return BadRequest();
        }

        //Date and Time check
        DateTime dateAndTime;
        if (!DateTime.TryParseExact(seminarForm.DateAndTime, dateAndTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
        {
            ModelState.AddModelError(nameof(SeminarAddViewModel.DateAndTime), $"Invalid date! Format must be {dateAndTimeFormat}");
        }
        if (dateAndTime < DateTime.Now)
        {
            ModelState.AddModelError(nameof(SeminarAddViewModel.DateAndTime), "Date and Time must be in the future!");
        }

        // Returning the User to the same page if some of the fields are not valid
        if (!ModelState.IsValid)
        {
            seminarForm.Categories = await GetCategories();
            return View(seminarForm);
        }

        //Creating a new seminar object
        var NewSeminar = new Seminar
        {
            Topic = seminarForm.Topic,
            Lecturer = seminarForm.Lecturer,
            Details = seminarForm.Details,
            DateAndTime = dateAndTime,
            Duration = seminarForm.Duration,
            CategoryId = seminarForm.CategoryId,
            OrganizerId = GetUserId()
        };

        //Adding the seminar to the database
        await data.AddAsync(NewSeminar);
        await data.SaveChangesAsync();

        //Redirecting the User to the All Seminars page
        return RedirectToAction(nameof(All));
    }

    //Method for the button Edit in the All Seminars page
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        //Getting the seminar by the given id that the User wants to edit
        var targetSeminar = await data.Seminars
            .AsNoTracking()
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();

        //Seminar not found
        if (targetSeminar == null)
        {
            return BadRequest();
        }

        //If a User is not the organizer of the seminar, he cannot edit it
        if (targetSeminar.OrganizerId != GetUserId())
        {
            return Unauthorized();
        }

        // Filling the form with the current values of the seminar
        var seminarToEdit = await data.Seminars
            .Where(s => s.Id == id)
            .Select(s => new SeminarEditViewModel
            {
                Id = s.Id,
                Topic = s.Topic,
                Lecturer = s.Lecturer,
                Details = s.Details,
                DateAndTime = s.DateAndTime.ToString(dateAndTimeFormat),
                Duration = s.Duration,
                CategoryId = s.CategoryId,
                Categories = GetCategories().Result
            })
            .FirstOrDefaultAsync();

        //Null check
        if (seminarToEdit == null)
        {
            return BadRequest();
        }

        return View(seminarToEdit);

    }

    //Method for the button Edit in the All Seminars page
    [HttpPost]
    public async Task<IActionResult> Edit(SeminarEditViewModel seminarForm)
    {
        //Validation of the model that the User has entered
        //Null check
        if (seminarForm == null)
        {
            return BadRequest();
        }

        //Date and Time check
        DateTime dateAndTime;
        if (!DateTime.TryParseExact(seminarForm.DateAndTime, dateAndTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
        {
            ModelState.AddModelError(nameof(SeminarEditViewModel.DateAndTime), $"Invalid date! Format must be {dateAndTimeFormat}");
        }
        if (dateAndTime < DateTime.Now)
        {
            ModelState.AddModelError(nameof(SeminarEditViewModel.DateAndTime), "Date and Time must be in the future!");
        }

        // Returning the User to the same page if some of the fields are not valid
        if (!ModelState.IsValid)
        {
            seminarForm.Categories = await GetCategories();
            return View(seminarForm);
        }

        //Getting the seminar by the given id that the User wants to edit
        var EditSeminar = await data.Seminars
            .Where(s => s.Id == seminarForm.Id)
            .FirstOrDefaultAsync();

        //Null check
        if (EditSeminar == null)
        {
            return BadRequest();
        }

        //Updating the seminar with the new values
        EditSeminar.Topic = seminarForm.Topic;
        EditSeminar.Lecturer = seminarForm.Lecturer;
        EditSeminar.Details = seminarForm.Details;
        EditSeminar.DateAndTime = dateAndTime;
        EditSeminar.Duration = seminarForm.Duration;
        EditSeminar.CategoryId = seminarForm.CategoryId;

        //Saving the changes to the database
        await data.SaveChangesAsync();

        //Redirecting the User to the All Seminars page
        return RedirectToAction(nameof(All));
    }
    //Method for the button Join in the All Seminars page
    [HttpPost]
    public async Task<IActionResult> Join(int id)
    {
        var currentUserId = GetUserId();

        //Checking if there is't a Seminar with the given id
        if (!await data.Seminars
            .AsNoTracking()
            .AnyAsync(s => s.Id == id))
        {
            return BadRequest();
        }

        //Checking if the User is already a participant in the seminar
        if (await data.SeminarsParticipants
             .AsNoTracking()
             .AnyAsync(sp => sp.SeminarId == id && sp.ParticipantId == currentUserId))
        {
            return RedirectToAction(nameof(All));
        }

        //Joining the seminar
        var seminarParticipant = new SeminarParticipant
        {
            SeminarId = id,
            ParticipantId = currentUserId
        };

        //Saving the changes to the database
        await data.SeminarsParticipants.AddAsync(seminarParticipant);
        await data.SaveChangesAsync();

        //Redirecting the User to the All Seminars page
        return RedirectToAction(nameof(Joined));
    }
    //Method for the button My Seminars
    [HttpGet]
    public async Task<IActionResult> Joined()
    {
        //Getting the current User Id
        var currentUserId = GetUserId();

        //Getting all the seminars that the User has joined
        var joinedSeminars = await data.SeminarsParticipants
            .AsNoTracking()
            .Where(sp => sp.ParticipantId == currentUserId)
            .Select(sp => new SeminarJoinedViewModel
            {
                Id = sp.Seminar.Id,
                Topic = sp.Seminar.Topic,
                Lecturer = sp.Seminar.Lecturer,
                Category = sp.Seminar.Category.Name,
                DateAndTime = sp.Seminar.DateAndTime.ToString(dateAndTimeFormat),
                Organizer = sp.Seminar.Organizer.UserName
            })
            .ToListAsync();

        return View(joinedSeminars);
    }

    //Method for the button Unsubscribe in the My Seminars page
    [HttpPost]
    public async Task<IActionResult> Leave(int id)
    {
        var currentUserId = GetUserId();

        //Checking if there is't a Seminar with the given id
        if (!await data.Seminars
            .AsNoTracking()
            .AnyAsync(s => s.Id == id))
        {
            return BadRequest();
        }

        //Checking if the User is a participant in the seminar
        var seminarParticipant = await data.SeminarsParticipants
            .Where(sp => sp.SeminarId == id && sp.ParticipantId == currentUserId)
            .FirstOrDefaultAsync();

        //If the User is not a participant in the seminar
        if (seminarParticipant == null)
        {
            return BadRequest();
        }

        //Leaving the seminar
        data.SeminarsParticipants.Remove(seminarParticipant);

        //Saving the changes to the database
        await data.SaveChangesAsync();

        //Redirecting the User to the Joined Seminars page
        return RedirectToAction(nameof(Joined));
    }
    //Method for the button Details in the All Seminars page
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var seminarDetails = await data.Seminars
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new SeminarDetailsViewModel
            {
                Id = s.Id,
                Topic = s.Topic,
                Lecturer = s.Lecturer,
                Details = s.Details,
                DateAndTime = s.DateAndTime.ToString(dateAndTimeFormat),
                Duration = s.Duration,
                Category = s.Category.Name,
                Organizer = s.Organizer.UserName
            })
            .FirstOrDefaultAsync();

        //Checking if there is't a Seminar with the given id
        if (seminarDetails == null)
        {
            return BadRequest();
        }

        //Returning the details of the seminar
        return View(seminarDetails);
    }
    //Method for the button Delete button that redirects to the DeleteConfirmed page
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var targetSeminar = await data.Seminars
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new SeminarDeleteViewModel
            {
                Id = s.Id,
                Topic = s.Topic,
                DateAndTime = s.DateAndTime
            })
            .FirstOrDefaultAsync();

        return View(targetSeminar);
    }

    //Method that deletes the seminar after the User has confirmed the deletion
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        //Getting the seminar by the given id that the User wants to delete
        var targetSeminar = await data.Seminars
            .AsNoTracking()
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();

        //Finding the Participants of the seminar
        var seminarParticipants = await data.SeminarsParticipants
            .Where(sp => sp.SeminarId == id)
            .ToListAsync();

        //Null check
        if (targetSeminar == null)
        {
            return BadRequest();
        }

        //If a User is not the organizer of the seminar, he cannot delete it
        if (targetSeminar.OrganizerId != GetUserId())
        {
            return Unauthorized();
        }

        //Checking if there are any participants in the seminar and removing them
        if (seminarParticipants != null && seminarParticipants.Any())
        {
            data.SeminarsParticipants.RemoveRange(seminarParticipants);
        }

        //Deleting the seminar
        data.Seminars.Remove(targetSeminar);
        await data.SaveChangesAsync();

        //Redirecting the User to the All Seminars page
        return RedirectToAction(nameof(All));
    }


    //Method to get the categories
    private async Task<ICollection<CategoryViewModel>> GetCategories()
    {
        return await data.Categories
            .Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    //Method to get the current User Id
    private string GetUserId()
    {
        string id = string.Empty;

        if (User != null)
        {
            id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        return id;
    }

}
