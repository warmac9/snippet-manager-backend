using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SnippetManager.Models;
using SnippetManager.Repository;
using System;

namespace SnippetManager.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUp model) {
            var res = await _accountRepository.SignUpAsync(model);

            if(res.Succeeded)
            {
                return Ok(res.Succeeded);
            }

            return Unauthorized(res.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody]SignIn model) {
            var res = await _accountRepository.SignInAsync(model);

            if(String.IsNullOrEmpty(res))
            {
                return Unauthorized();
            }

            return Ok(res);
        }
    }
}

