using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models.UserModels;
using DAL.Models;
using DAL.Repositories;
//using Fnews.Models;
//using Fnews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fnews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IUserLogic _userLogic;
        private readonly IConfiguration _config;

        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            User user = _userLogic.GetUserById(id);
            if (user != null)
            {
                return NotFound("This user is not exist");
            }
            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserUpdateModel userUpdate)
        {
            User user = _userLogic.GetUserById(userUpdate.UserId);
            if(user != null)
            {
                return NoContent();
            }
            user.Email = userUpdate.Email;
            user.UserTag = userUpdate.UserTag;
            _userLogic.UpdateUser(user);
            return Ok("User updated");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginModel user)
        {
            var userFromData = await _repo.Login(user.Email.ToLower(), user.Password);

            if (userFromData == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromData.UserId.ToString()),
                new Claim(ClaimTypes.Email, userFromData.Email),
               // new Claim(ClaimTypes.Role, (string)userFromData.IsAdmin)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)
            );

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha512Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }




    }

}
