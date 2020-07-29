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
