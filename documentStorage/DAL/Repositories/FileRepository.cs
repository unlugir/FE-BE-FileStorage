using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FileRepository: IFileRepository
    {
        private StorageContext _db;
        public FileRepository(StorageContext db)
        {
            _db = db;
        }

        public void Add(FileEntity file)
        {
            _db.Files.Add(file);
        }
        public void Delete(FileEntity file)
        {
            _db.Files.Remove(file);
        }
        public void Update(FileEntity file)
        {
            var entity = _db.Entry<FileEntity>(file);
            if (entity.State == EntityState.Detached)
                _db.Attach(file);
            _db.Files.Update(file);
        }
        public async Task<FileEntity> GetByIdAsync(Guid id)
        {
            return await _db.Files.FindAsync(id);
        }
        public async Task<IEnumerable<FileEntity>> GetAllAsync() 
        {
            return await _db.Files.Include(file=> file.User).ToListAsync();
        }
    }
}
