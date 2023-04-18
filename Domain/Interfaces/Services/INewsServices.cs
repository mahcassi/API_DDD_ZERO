using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface INewsServices
    {
        Task CreateNews(News news);
        Task UpdateNews(News news);
        Task<List<News>> GetAllActiveNews();
    }
}
