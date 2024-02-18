namespace SeminarHub.Models.Type;

using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Common.Constants.CategoryConstants;

public class CategoryViewModel
{
    public int Id { get; set; }

    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;
}
