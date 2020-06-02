using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private StorageContext _context { get; }
        public UnitOfWork(
            StorageContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IFileRepository fileRepository)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            _context = context;
            Files = fileRepository;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public IFileRepository Files { get; }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() != 0;
        }

    }
}
