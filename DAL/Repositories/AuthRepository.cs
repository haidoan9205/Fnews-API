using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FnewsContext _context;

        
        public  AuthRepository(FnewsContext context)
        {
            _context = context;

        }
       

        public async Task<User> Login(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x =>
                x.Email == email);

            if (user.Password == password)
                return user;

            return null;
        }
        public async Task<bool> UserExists(string email)
        {
            if (await _context.User.AnyAsync(x => x.Email == email))
                return true;

            return false;
        }

       











        //}
    }
}
