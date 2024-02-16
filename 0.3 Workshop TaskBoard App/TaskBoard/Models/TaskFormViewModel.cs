using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

namespace TaskBoard.Models;

public class TaskFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
    [StringLength(DataConstants.Task.TitleMaxLenght, MinimumLength = DataConstants.Task.TitleMinLenght,ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = ErrorMessages.RequiredErrorMessage)]
    [StringLength(DataConstants.Task.DescriptionMaxLenght, MinimumLength = DataConstants.Task.DescriptionMinLenght,ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
    public string Description { get; set; } = string.Empty;

    public int? BoardId { get; set; }

    public IEnumerable<TaskBoardViewModel> Boards { get; set; } = new List<TaskBoardViewModel>();
}
