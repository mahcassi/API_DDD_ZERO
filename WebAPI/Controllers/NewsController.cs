using Application.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsApplication _INewsApplication;
        public NewsController(INewsApplication INewsApplication)
        {
            _INewsApplication = INewsApplication;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ActiveNews")]
        public async Task<List<News>> GetAllNews()
        {
            return await _INewsApplication.GetAllActiveNews();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/CreateNews")]
        public async Task<List<Notify>> CreateNews(NewsModel news)
        {
            var currentNews = new News();
            currentNews.Title = news.Title;
            currentNews.Information = news.Information;
            currentNews.UserId = await ReturnUserId();
            await _INewsApplication.CreateNews(currentNews);

            return currentNews.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/UpdateNews")]
        public async Task<List<Notify>> UpdateNews(NewsModel news)
        {
            var currentNews = await _INewsApplication.FindbyId(news.NewsId);
            currentNews.Title = news.Title;
            currentNews.Information = news.Information;
            currentNews.UserId = await ReturnUserId();
            await _INewsApplication.CreateNews(currentNews);

            return currentNews.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/ExcluirNoticia")]
        public async Task<List<Notify>> DeleteNews(NewsModel news)
        {
            var currentNews = await _INewsApplication.FindbyId(news.NewsId);
            await _INewsApplication.Delete(currentNews);

            return currentNews.Notifications;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/FindbyId")]
        public async Task<Notify> FindbyId(NewsModel news)
        {
            var currentNews = await _INewsApplication.FindbyId(news.NewsId);

            return currentNews;
        }

        private async Task<string> ReturnUserId()
        {
            if (User != null)
            {
                var idUser = User.FindFirst("idUser");
                return idUser.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
