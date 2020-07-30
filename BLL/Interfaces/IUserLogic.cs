using BLL.Models.UserModels;
using DAL.Models;
using System.Linq;

namespace BLL.Interfaces
{
    public interface IUserLogic
    {

        public User GetUserById(int id);
        public bool UpdateUser(User user);

        public bool CreateUser(User user);
        public IQueryable<User> GetAllUsers();


    }
}
