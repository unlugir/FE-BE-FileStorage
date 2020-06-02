using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResult<User>> GetUserFromContext(ClaimsPrincipal claimUser) 
        {
            try
            {
                var identityUser = await _uow.UserManager.GetUserAsync(claimUser);
                if (identityUser != null)
                    return new ServiceResult<User>(_mapper.Map<User>(identityUser));
                return new ServiceResult<User>(new List<string> { "Unable to identify user" });
            }
            catch (Exception e)
            {
                return new ServiceResult<User>(new List<string> { e.Message });
            }
        }
    }
}
