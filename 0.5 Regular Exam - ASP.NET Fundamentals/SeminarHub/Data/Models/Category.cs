using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Common.Constants.CategoryConstants;

namespace SeminarHub.Data.Models;

public class Category
{
    [Key]
    [Comment("Category Identifier")]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment("Category Name")]
    public string Name { get; set; } = null!;

    public ICollection<Seminar> Seminars { get; set; } = new HashSet<Seminar>();
}