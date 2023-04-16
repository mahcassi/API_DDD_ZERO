using Domain.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NewsServices : INewsServices
    {
        private readonly INews _INews;
        public NewsServices(INews INews)
        {
            _INews = INews;
        }
        public async Task CreateNews(News news)
        {
            var validateTitle = news.ValidatePropertyString(news.Title, "Title");
            var validateInformatio = news.ValidatePropertyString(news.Information, "Information");
            if (validateTitle && validateInformatio)
            {
                news.UpdateDate = DateTime.Now;
                news.CreateDate = DateTime.Now;
                news.Active = true;

                await _INews.Create(news);
            }
        }

        public async Task<List<News>> GetAllActiveNews()
        {
            return await _INews.GetAllNews(n => n.Active);
        }

        public async Task UpdateNews(News news)
        {
            var validateTitle = news.ValidatePropertyString(news.Title, "Title");
            var validateInformatio = news.ValidatePropertyString(news.Information, "Information");
            if (validateTitle && validateInformatio)
            {
                news.UpdateDate = DateTime.Now;
                news.CreateDate = DateTime.Now;
                news.Active = true;

                await _INews.Update(news);
            }
        }
    }
}
