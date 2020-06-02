using BLL.Infrastructure;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFileService
    {
        Task<ServiceResult<FileData>> CreateFileData(FileData file);
        Task<ServiceResult<FileData>> UploadFileAsync(IFormFile file, User uploaderUser);
        Task<ServiceResult<FileData>> UpdateFileAsync(FileData fileData);
        Task<ServiceResult<FileData>> DeleteFileAsync(Guid id);
        Task<IEnumerable<FileData>> GetAllFilesAsync();
        Task<ServiceResult<FileStream>> GetFileStreamByIdAsync(Guid id);
        Task<FileData> GetFileDataByIdAsync(Guid id);
    }
}
