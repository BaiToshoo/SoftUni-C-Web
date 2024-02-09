using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.infrastructure.Constants;
/// <summary>
/// Validation constants for the Post model
/// </summary>
public static class ValidationConstants
{
    /// <summary>
    /// Maimal Post title length
    /// </summary>
    public const int TitleMaxLength = 50;

    /// <summary>
    /// Minimal Post title length
    /// </summary>
    public const int TitleMinLength = 10;

    /// <summary>
    /// Maximal Post content length
    /// </summary>
    public const int ContentMaxLength = 1500;

    /// <summary>
    /// Minimal Post content length
    /// </summary>
    public const int ContentMinLength = 30;

    /// <summary>
    /// Required Error message text
    /// </summary>
    public const string RequireErrorMessage = "The {0} field is required";

    public const string StringLengthErrorMassage = "The {0} field must be between {2} and {1} characters long";
}
