using BLL.Interfaces;
using BLL.Models.CommentModels;
using DAL.Models;
using DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.BusinessLogics
{
    public class CommentLogic : ICommentLogic
    {
        public readonly IUnitOfWork _unitOfWork;

        public CommentLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateNewComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }

        /*    public bool CreateNewComment(CommentModel comment)
   {
       bool check = false; 
       if(comment != null)
       {
           Comment commentModel = new Comment()
           {

           }

       }
   }*/

        public bool DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public CommentLogic GetCommentById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateComment(CommentLogic comment)
        {
            throw new NotImplementedException();
        }
    }
}
