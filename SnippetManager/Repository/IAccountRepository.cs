using System;
using Microsoft.AspNetCore.Identity;
using SnippetManager.Models;

namespace SnippetManager.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUp model);

        Task<String> SignInAsync(SignIn model);
    }
}
