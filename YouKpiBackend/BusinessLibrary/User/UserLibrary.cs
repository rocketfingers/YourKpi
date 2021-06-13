using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YouKpiBackend.BusinessLibrary.User;
using YouKpiBackend.DbContexts;
using YouKpiBackend.Helpers;
using YouKpiBackend.Helpers.Extensions;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.BusinessLibrary
{
    public class UserLibrary
    {
        readonly YoukpiContext _context;

        public UserLibrary(YoukpiContext context)
        {
            _context = context;
        }
        public string BuildToken(UserPrepareTokenModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ??""),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            if (user.IsAdministrator)
            {
                claims.Add(new Claim("role", "Administrator"));

            }
            string jwtKey = ConfigHelper.AppSetting("Jwt", "Key");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null, null,
              claims,
              expires: DateTime.Now.AddMinutes(250),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<UserPrepareTokenModel> Authenticate(string login, string password)
        {
            var hashedPassword = password.HashSha256();
            var prac = await _context.Pracownik.FirstOrDefaultAsync(p => p.Login == login);
            if (prac == null)
            {
                throw new BadLoginOrPasswordException();
            }
            if (prac.Password != hashedPassword)
            {
                throw new BadLoginOrPasswordException();
            }

            UserPrepareTokenModel userModel = new UserPrepareTokenModel()
            {
                Id = prac.Id,
                Login = prac.Login,
                Email = prac.Email,
                IsAdministrator = prac.IsAdministrator,
                Name = prac.Name
            };

            return userModel;
        }
    }
}
