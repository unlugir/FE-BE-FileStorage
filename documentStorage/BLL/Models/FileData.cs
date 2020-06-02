using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class FileData
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Extention { get; set; }
        public DateTime UploadDate { get; set; }
        public string Category { get; set; }
        public User User { get; set; }
    }
}
