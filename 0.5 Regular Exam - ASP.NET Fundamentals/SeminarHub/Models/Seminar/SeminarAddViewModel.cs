using SeminarHub.Models.Type;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Common.Constants.SeminarConstants;

namespace SeminarHub.Models.Seminar;

public class SeminarAddViewModel
{

    [Required]
    [StringLength(TopicMaxLength, MinimumLength = TopicMinLength)]
    public string Topic { get; set; } = null!;

    [Required]
    [StringLength(LecturerMaxLength, MinimumLength = LecturerMinLength)]
    public string Lecturer { get; set; } = null!;

    [Required]
    [StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength)]
    public string Details { get; set; } = null!;

    [Required]
    public string DateAndTime { get; set; } = null!;

    [Range(DurationMinValue, DurationMaxValue)]
    public int? Duration { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public ICollection<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

}
