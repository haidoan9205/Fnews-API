using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
