using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Login(string email, string password);
    }
}
