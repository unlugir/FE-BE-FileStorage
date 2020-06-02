using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FileService: IFileService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        private static string storagePath = @"E:\univer\1Univer2Semestr\req_anal\homework\DocumentStorage\StoredFiles\";
        public FileService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResult<FileData>> DeleteFileAsync(Guid id)
        {
            try
            {
                var fileToDelete = await _uow.Files.GetByIdAsync(id);
                _uow.Files.Delete(fileToDelete);
                await _uow.SaveChangesAsync();
                File.Delete(storagePath + fileToDelete.Name);
                return new ServiceResult<FileData>(_mapper.Map<FileData>(fileToDelete));
            }
            catch (Exception e)
            {
                return new ServiceResult<FileData>(new List<string> { e.Message });
            }
        }

        public async Task<IEnumerable<FileData>> GetAllFilesAsync()
        {
            return _mapper.Map<IEnumerable<FileData>>(await _uow.Files.GetAllAsync());
        }

        public async Task<ServiceResult<FileStream>> GetFileStreamByIdAsync(Guid id)
        {
            try
            {
                var fileData = await _uow.Files.GetByIdAsync(id);
                if (fileData == null)
                    throw new Exception("File not found");
                return new ServiceResult<FileStream>(File.OpenRead(storagePath + fileData.Name));
            }
            catch (Exception e)
            {
                return new ServiceResult<FileStream>(new List<string> { e.Message });
            }
        }

        public Task<ServiceResult<FileData>> UpdateFileAsync(FileData fileData)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<FileData>> CreateFileData(FileData file)
        {
            var fileData = _mapper.Map<FileEntity>(file);
            _uow.Files.Add(_mapper.Map<FileEntity>(fileData));
            await _uow.SaveChangesAsync();
            return new ServiceResult<FileData>(_mapper.Map<FileData>(fileData));
        }
        public async Task<ServiceResult<FileData>> UploadFileAsync(IFormFile file, User uploaderUser)
        {
            try
            {
                if (file!=null)
                    using (var stream = new FileStream(storagePath+file.FileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                var fileData = new FileEntity()
                {
                    Extention = file.ContentType,
                    Name = file.FileName,
                    UploadDate = DateTime.Now,
                    UserId = uploaderUser.Id
                };
                _uow.Files.Add(_mapper.Map<FileEntity>(fileData));
                await _uow.SaveChangesAsync();
                return new ServiceResult<FileData>(_mapper.Map<FileData>(fileData));
            }
            catch (Exception e)
            {
                return new ServiceResult<FileData>(new List<string> { e.Message });
            }
        }

        public async Task<FileData> GetFileDataByIdAsync(Guid id)
        {
            return _mapper.Map<FileData>(await _uow.Files.GetByIdAsync(id));
        }
    }
}
