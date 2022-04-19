using System;
using System.ComponentModel.DataAnnotations;

namespace SnippetManager.Models
{
    public record SignUp
    {
        [Required, EmailAddress]
        public string Email { get; init; }

        [Required, Compare("ConfirmPassword")]
        public string Password { get; init; }

        [Required]
        public string ConfirmPassword { get; init; }
    }
}
