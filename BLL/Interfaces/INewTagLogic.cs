using BLL.Models.NewsTagModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BLL.Interfaces
{
    public interface INewTagLogic
    {
        public IQueryable<NewsTag> GetNewsTag();
        public NewsTag GetModelsById(int id);
        public bool DeleteTagNews(int tagId);
        public bool CreateTagNews(NewsTagModel newsTag);
    }
}
