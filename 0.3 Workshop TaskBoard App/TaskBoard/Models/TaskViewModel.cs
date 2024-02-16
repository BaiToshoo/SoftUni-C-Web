using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

namespace TaskBoard.Models;

public class TaskViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(DataConstants.Task.TitleMaxLenght,MinimumLength = DataConstants.Task.TitleMinLenght)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(DataConstants.Task.DescriptionMaxLenght,MinimumLength = DataConstants.Task.DescriptionMinLenght)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Owner { get; set; } = string.Empty;
}
