using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Exceptions;
using Aqua_Sharp_Backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Models.ViewModels.Config;

namespace Aqua_Sharp_Backend.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IPasswordHasher<Config> _passwordHasher;
        private readonly Context _context;
        private readonly AuthenticationSettings _authenticationSettings;
        public Task ChangePassword()
        {
            throw new NotImplementedException();
        }

        public Task ChangeQuestion()
        {
            throw new NotImplementedException();
        }

        
        public Task Configure()
        {
            throw new NotImplementedException();
        }

        
        public ConfigService(Context context, IPasswordHasher<Config> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;

        }

        public string GenerateJwt(LoginViewModel vm)
        {
            var config = _context.Config.FirstOrDefault(u => u.ConfigId == 1);
            var aquariums = _context.Aquarium.ToList();
            var devices = _context.Devices.ToList();



            var result = _passwordHasher.VerifyHashedPassword(config, config.Password, vm.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequest400Exception("Invaild password");
            }


            var claims = new List<Claim>()
                {
                
                new Claim(ClaimTypes.NameIdentifier, config.ConfigId.ToString()),
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);




        }
    }
}
