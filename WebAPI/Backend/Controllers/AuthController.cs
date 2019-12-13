using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using Utility.Enums;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _config = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            LoginResult loginResult = await _authService.LoginAsync(loginVM);

            switch (loginResult)
            {
                case LoginResult.Succeeded:
                    UserViewModel userVM = await _authService.GetByUserNameAsync(loginVM.UserName);
                    return Ok(new LoginResponse(true, await GenerateJwtAsync(userVM), null));
                case LoginResult.IsLockedOut:
                    return Ok(new LoginResponse(false, null, "Tài khoản đã bị khóa!"));
                case LoginResult.Unauthorized:
                    return Unauthorized();
                default:
                    return Ok(new LoginResponse(false, null, "Tài khoản hoặc mật khẩu không đúng!"));
            }
        }

        private async Task<string> GenerateJwtAsync(UserViewModel user)
        {   
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("fullName", user.FullName)
            };

            IList<string> roles = await _authService.GetRolesAsync(user);
            foreach(string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}