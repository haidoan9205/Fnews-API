using BLL.Interfaces;
using DAL.Models;
using DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessLogics
{
    public class UserLogic : IUserLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateUser(User userModel)
        {
            bool check = false;
            if (userModel != null)
            {
                try
                {
                    User user = new User()
                    {

                        Email = userModel.Email,
                        IsAdmin = userModel.IsAdmin,
                        GroupId = userModel.GroupId,
                        Password = userModel.Password,
                        Group = userModel.Group,
                        IsActive = true,
                    };
                    _unitOfWork.GetRepository<User>().Insert(user);
                    _unitOfWork.Commit();
                    check = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }


            }
            return check;
        }

        public IQueryable<User> GetAllUsers()
        {
            IQueryable<User> user = _unitOfWork.GetRepository<User>().GetAll();
            return user;
        }

        public User GetUserById(int id)
        {
            User user = _unitOfWork.GetRepository<User>().FindById(id);
            return user;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
