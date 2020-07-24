using BLL.BusinessLogics;
using BLL.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICommentLogic
    {
        public bool CreateNewComment(CommentModel comment);
        public bool DeleteComment(int id);
        public bool UpdateComment(CommentLogic comment);
        public CommentLogic GetCommentById(int id);
    }
}
