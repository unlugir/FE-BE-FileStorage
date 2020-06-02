using BLL.Infrastructure;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<User>> GetUserFromContext(ClaimsPrincipal claimUser);
    }
}
