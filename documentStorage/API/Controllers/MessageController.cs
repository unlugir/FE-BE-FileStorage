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
using System.Security.Claims;

namespace API.Controllers
{
    public class MessageDto
    {
        public int UserId { get; set; }
        public string Text { get; set; }
    }

    public interface IMessageService
        {}

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController:ControllerBase
    {

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]MessageDto newMessage)
        {
            string userName = HttpContext.User.Claims.Where(claim => claim.Type == ClaimTypes.Name).First().Value;

            //var user = userService.GetUserByName(userName);
            //newMessage.UserId = user.Id;
            //
            IMessageService service;
            //service.CreateMessage(newMessage);
            return Ok();
        }
    }
}
