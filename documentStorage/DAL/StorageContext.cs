using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class StorageContext: IdentityDbContext
    {
        public DbSet<FileEntity> Files { get; set; }
        public StorageContext(DbContextOptions options)
            :base(options)
        {   
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FileEntity>()
                .HasKey(file => file.Id);
           
            builder.Entity<FileEntity>()
                .HasOne(file => file.User)
                .WithMany()
                .HasForeignKey(file => file.UserId);
            builder.Entity<FileEntity>()
                .Property(file => file.Name)
                .IsRequired();
        }

    }
}
