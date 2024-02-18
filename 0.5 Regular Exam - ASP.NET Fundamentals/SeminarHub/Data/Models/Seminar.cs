using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.Common.Constants.SeminarConstants;

namespace SeminarHub.Data.Models;

public class Seminar
{
    [Key]
    [Comment("Seminar Identifier")]
    public int Id { get; set; }

    [Required]
    [MaxLength(TopicMaxLength)]
    [Comment("Seminar Topic")]
    public string Topic { get; set; } = null!;

    [Required]
    [MaxLength(LecturerMaxLength)]
    [Comment("Seminar Lecturer")]
    public string Lecturer { get; set; } = null!;

    [Required]
    [MaxLength(DetailsMaxLength)]
    [Comment("Seminar Details")]
    public string Details { get; set; } = null!;

    [Required]
    [Comment("Seminar Organizer Identifier")]
    public string OrganizerId { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(OrganizerId))]
    [Comment("Seminar Organizer")]
    public IdentityUser Organizer { get; set; } = null!;

    [Required]
    [Comment("Seminar Date and Time")]
    public DateTime DateAndTime { get; set; }

    [MaxLength(DurationMaxValue)]
    [Comment("Seminar Duration")]
    public int? Duration { get; set; }

    [Required]
    [Comment("Seminar Category Identifier")]
    public int CategoryId { get; set; }

    [Required]
    [ForeignKey(nameof(CategoryId))]
    [Comment("Seminar Category")]
    public Category Category { get; set; } = null!;

    [Comment("Seminar Participants")]
    public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new HashSet<SeminarParticipant>();
}
