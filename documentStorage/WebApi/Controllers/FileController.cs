using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            Timer t = new Timer();
            t.Start();
            var files = HttpContext.Request.Form.Files;
            if (files.Count == 0)
                return BadRequest("0 files received");
            foreach(var file in files)
            {
                using (var stream = new FileStream($@"Files\{file.FileName}", FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            t.Stop();
            return Ok(t.Interval);
        }
    }
}