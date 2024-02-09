using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForumApp.infrastructure.Constants.ValidationConstants;

namespace ForumApp.Core.Models;

/// <summary>
/// Post data transfer model
/// </summary>
public class PostModel
{
    /// <summary>
    /// Post Identificator
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Post Title
    /// </summary>
    [Required(ErrorMessage = RequireErrorMessage)]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength,ErrorMessage = StringLengthErrorMassage)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Post Content
    /// </summary>
    [Required(ErrorMessage = RequireErrorMessage)]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = StringLengthErrorMassage)]
    public string Content { get; set; } = string.Empty;
}
