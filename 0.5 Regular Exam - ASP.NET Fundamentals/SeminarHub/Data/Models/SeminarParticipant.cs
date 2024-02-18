using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models;

public class SeminarParticipant
{
    [Required]
    [Comment("Seminar Identifier")]
    public int SeminarId { get; set; }

    [ForeignKey(nameof(SeminarId))]
    [Comment("Seminar")]
    public Seminar Seminar { get; set; } = null!;

    [Required]
    [Comment("Participant Identifier")]
    public string ParticipantId { get; set; } = null!;

    [ForeignKey(nameof(ParticipantId))]
    [Comment("Participant")]
    public IdentityUser Participant { get; set; } = null!;


}