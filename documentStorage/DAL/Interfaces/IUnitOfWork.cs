using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        UserManager<IdentityUser> UserManager { get; }
        SignInManager<IdentityUser> SignInManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IFileRepository Files { get; }
        Task<bool> SaveChangesAsync();

    }
}
