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
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private IAccountService accountService;
        public UserController(IUserService userService, IAccountService accountService)
        {
            this.userService = userService;
            this.accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var result = await userService.GetUserFromContext(User);
            if (!result.Succeded)
                return BadRequest();
            return Ok(result.Result);
        }
    }
}
