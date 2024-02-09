using static ForumApp.infrastructure.Constants.ValidationConstants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.infrastructure.Data.Models;
[Comment("Posts table")]
public class Post
{
    [Key]
    [Comment("Post identifier")]
    public int Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment("Post title")]
    public required string Title { get; set; }
    [Required]
    [MaxLength(ContentMaxLength)]
    [Comment("Post content")]
    public required string Content { get; set; }
}
