using BLL.Models.BookMarkModels;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Interfaces
{
    public interface IBookMarkLogic
    {
        public bool CreateNewBookMark(BookMarkModel bookMark);
        public bool DeleteBookMark(int id);

        public IQueryable<Bookmark> GetAllBookmarks();

    }
}
