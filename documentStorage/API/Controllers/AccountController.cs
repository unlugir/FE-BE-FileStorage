using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model) 
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await accountService.LoginAsync(model);
            if (result.Succeded)
            {
                var token = await accountService.GenerateTokenAsync(result.Result);
                return Ok(new { token });
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await accountService.RegisterAsync(model);
            if(result.Succeded)
            {
                return Created("",result.Result);
            }
            return BadRequest(result.Errors);
        }
    }
}
