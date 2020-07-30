using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models.UserModels;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
//using Fnews.Models;
//using Fnews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fnews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FnewsContext _context;
        private readonly IAuthRepository _repo;
        private readonly IUserLogic _userLogic;
        private readonly IConfiguration _config;

        public UsersController(IUserLogic userLogic, IAuthRepository authRepo, IConfiguration config, FnewsContext context)
        {
            _userLogic = userLogic;
            _context = context;
            _repo = authRepo;
            _config = config;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserUpdateModel user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                 _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginModel user)
        {
            var userFromData =await _repo.Login(user.Email.ToLower(), user.Password);
  
            if (userFromData == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromData.UserId.ToString()),
                new Claim(ClaimTypes.Email, userFromData.Email.ToString()),
                new Claim(ClaimTypes.Role, userFromData.IsAdmin.ToString()),
            };

          
            //var userStore = new UserStore<User>(_context);

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


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            //Console.WriteLine(ClaimTypes.Role);
            List<User> users = _userLogic.GetAllUsers().ToList();
            if (users == null)
            {
                return BadRequest("Error");
            }
            if (users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("null");
            }
            bool check = _userLogic.CreateUser(user);
            if (!check)
            {
                return BadRequest("Cannot create a new user");
            }
            return Ok(user);
        }


        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }





    }

}
