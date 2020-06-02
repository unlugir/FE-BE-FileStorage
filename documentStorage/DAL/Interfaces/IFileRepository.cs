using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IFileRepository
    {
        void Add(FileEntity file);
        void Update(FileEntity file);
        void Delete(FileEntity file);
        Task<FileEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<FileEntity>> GetAllAsync();
    }
}
