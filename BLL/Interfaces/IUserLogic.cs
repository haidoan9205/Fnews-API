using BLL.Models.UserModels;
using DAL.Models;


namespace BLL.Interfaces
{
    public interface IUserLogic
    {

        public User GetUserById(int id);
        public bool UpdateUser(User user);


    }
}
