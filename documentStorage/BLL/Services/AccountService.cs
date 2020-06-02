using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Constants;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService:IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public AccountService(
            IMapper mapper,
            IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        private async Task<List<Claim>> GetUserClaimsAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _uow.UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var identityUser = _mapper.Map<IdentityUser>(user);
            var userClaims = await GetUserClaimsAsync(identityUser);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtHelper.Issuer,
                Audience = JwtHelper.Audience,
                Subject = new ClaimsIdentity(userClaims),
                Expires = JwtHelper.TokenLifetTime,
                SigningCredentials = JwtHelper.CreateCredentials()
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        public async Task<ServiceResult<User>> RegisterAsync(RegistrationModel newUser)
        {
            try
            {
                var userToCreate = new IdentityUser() { Email = newUser.Email, UserName = newUser.Email };
                var result = await _uow.UserManager.CreateAsync(userToCreate, newUser.Password);
                if (result.Succeeded)
                {
                    var identityUser = await _uow.UserManager.FindByNameAsync(userToCreate.UserName);
                    //await _uow.UserManager.AddToRoleAsync(identityUser, Roles.Member);
                    return new ServiceResult<User>(_mapper.Map<User>(identityUser));
                }
                return new ServiceResult<User>(result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResult<User>(new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResult<User>> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var identityUser = await _uow.UserManager.FindByEmailAsync(loginModel.Email);
                if (identityUser == null)
                    return new ServiceResult<User>(new List<string> { "Invalid Email" });
                var result = await _uow.SignInManager.CheckPasswordSignInAsync(identityUser, loginModel.Password, false);
                if (result.Succeeded)
                {
                    return new ServiceResult<User>(_mapper.Map<User>(identityUser));
                }
                return new ServiceResult<User>(new List<string> { "Invalid credentials" });
            }
            catch (Exception ex)
            {
                return new ServiceResult<User>(new List<string> { ex.Message });
            }
        }


    }
}
