using System;
using System.ComponentModel.DataAnnotations;

namespace SnippetManager.Models
{
    public record SignIn
    {
        [Required, EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
