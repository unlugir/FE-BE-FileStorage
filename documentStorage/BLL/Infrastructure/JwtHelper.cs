using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    public static class JwtHelper
    {
        private static string Key = "HardTo_string123_";
        public static DateTime TokenLifetTime = DateTime.Now.AddDays(1);
        public static string HashAlgorithm = SecurityAlgorithms.HmacSha256;
        public static string Issuer = "http://issuer";
        public static string Audience = "http://audience";

        public static void ConfigureJwtOptions(JwtBearerOptions options)
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudience = Audience,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
            };
        }

        public static SigningCredentials CreateCredentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            return new SigningCredentials(securityKey, HashAlgorithm);
        }

    }
}
