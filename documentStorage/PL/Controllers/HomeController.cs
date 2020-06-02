using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        //[Route("/home")]
        public ActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostUpload()
        {
            var request = HttpContext.Request.Form;
            var file = request.Files[0];
            long size = file.Length;

            string path = @$"E:\univer\1Univer2Semestr\req_anal\homework\DocumentStorage\PL\Files\{Guid.NewGuid()}.tmp";
            // full path to file in temp location
            
            if (size > 0)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok(new {  size, path });
        }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            
        

    }
}