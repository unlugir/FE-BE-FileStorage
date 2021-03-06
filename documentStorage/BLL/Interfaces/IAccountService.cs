﻿using AutoMapper;
using BLL.Infrastructure;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<ServiceResult<User>> LoginAsync(LoginModel model);
        Task<ServiceResult<User>> RegisterAsync(RegistrationModel model);
    }
}
