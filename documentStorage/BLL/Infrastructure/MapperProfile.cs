using AutoMapper;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<IdentityUser, User>().ReverseMap();
            CreateMap<IdentityRole, Role>().ReverseMap();
            CreateMap<FileEntity, FileData>().ReverseMap();
        }
    }
}
