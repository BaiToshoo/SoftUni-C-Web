namespace SeminarHub.Models.Seminar;

using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Common.Constants.SeminarConstants;

public class SeminarDetailsViewModel : SeminarAllViewModel
{
    [StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength)]
    public string Details { get; set; } = null!;

    [Range(DurationMinValue, DurationMaxValue)]
    public int? Duration { get; set; }
}
