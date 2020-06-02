using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public DateTime UploadDate { get; set; }
        public string Category { get; set; }
        public IdentityUser User { get; set; }
    }
}
