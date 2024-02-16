namespace TaskBoard.Models;

using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

public class BoardViewModel
{
    public int Id { get; set; }
    [Required]
    [StringLength(DataConstants.Board.NameMaxLenght, MinimumLength =DataConstants.Board.NameMinLenght)]
    public string Name { get; set; } = string.Empty;
    public IEnumerable<TaskViewModel> Tasks { get; set;} = new List<TaskViewModel>();
}
