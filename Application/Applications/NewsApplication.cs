using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class NewsApplication : INewsApplication
    {
        private readonly INews _INews;
        private readonly INewsServices _INewsServices;

        public NewsApplication(INews INews, INewsServices INewsServices)
        {
            _INews= INews;
            _INewsServices = INewsServices;
        }

        public async Task<List<News>> GetAllActiveNews()
        {
            return await _INewsServices.GetAllActiveNews();
        }

        public async Task UpdateNews(News news)
        {
           await _INewsServices.UpdateNews(news);
        }


        public async Task CreateNews(News news)
        {
            await _INewsServices.CreateNews(news);
        }

        public async Task Create(News Obj)
        {
           await _INews.Create(Obj);
        }


        public async Task Delete(News Obj)
        {
            await _INews.Delete(Obj);
        }

        public async Task<News> FindbyId(int Id)
        {
            return await _INews.FindbyId(Id);
        }

        public async Task<List<News>> List()
        {
            return await _INews.List();
        }

        public async Task Update(News Obj)
        {
            await _INews.Update(Obj);
        }
       
    }
}
