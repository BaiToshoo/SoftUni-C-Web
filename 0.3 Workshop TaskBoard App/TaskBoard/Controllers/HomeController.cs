using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TaskBoardAppDbContext data;

    public HomeController(ILogger<HomeController> logger, TaskBoardAppDbContext context)
    {
        _logger = logger;
        data = context;
    }

    public async Task<IActionResult> Index()
    {
        var taskBoards = data.Boards
            .Select(b => b.Name)
            .Distinct()
            .ToList();

        var tasksCount = new List<HomeBoardModel>();

        foreach (var boardName in taskBoards)
        {
            var tasksInBoard = await data.Tasks
                .Where(t => t.Board.Name == boardName)
                .CountAsync();

            tasksCount.Add(new HomeBoardModel()
            {
                BoardName = boardName,
                TasksCount = tasksInBoard
            });
        }

        var userTaskCount = -1;

        if (User.Identity.IsAuthenticated)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userTaskCount = data.Tasks
                .Where(t => t.OwnerId == currentUserId)
                .Count();
        }

        var homeModel = new HomeViewModel()
        {
            AllTasksCount = data.Tasks.Count(),
            BoardsWithTasksCount = tasksCount,
            UserTasksCount = userTaskCount
        };

        return View(homeModel);
    }
}
