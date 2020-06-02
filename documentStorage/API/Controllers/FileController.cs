using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BLL.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService fileService;
        private IUserService userService;
        public FileController(
            IFileService fileService,
            IUserService userService)
        {
            this.fileService = fileService;
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await fileService.GetFileStreamByIdAsync(id);
            if (!result.Succeded)
                return Conflict(result.Errors);
            return File(result.Result, "application/octet-stream");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await fileService.GetAllFilesAsync());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadFiles()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
                return BadRequest("0 files received");





            var authResult = await userService.GetUserFromContext(User);
            if (!authResult.Succeded)
                return BadRequest(authResult.Errors);





            var uploadedFiles = new List<FileData>();
            foreach (var file in files)
            {
                var result = await fileService.UploadFileAsync(file, authResult.Result);
                if (result.Succeded)
                    uploadedFiles.Add(result.Result);
            }
            return Ok(uploadedFiles);
        }

        [HttpPut("{id}/category")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody]string category)
        {
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await fileService.DeleteFileAsync(id);
            if (!res.Succeded)
                return BadRequest(res.Errors);
            return Ok(id);
        }
    }
}