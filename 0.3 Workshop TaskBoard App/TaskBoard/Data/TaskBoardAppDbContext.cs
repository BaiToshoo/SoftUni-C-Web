using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Configuration;
using TaskBoard.Data.SeedData;
namespace TaskBoard.Data;
public class TaskBoardAppDbContext : IdentityDbContext
{
    public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        modelbuilder.ApplyConfiguration(new UserConfiguration());
        modelbuilder.ApplyConfiguration(new BoardConfiguration());
        modelbuilder.ApplyConfiguration(new TaskConfiguration());

        base.OnModelCreating(modelbuilder);
    }
    public DbSet<Board> Boards { get; set; } 
    public DbSet<Task> Tasks { get; set; }
}
