using Domain.Interfaces;
using Entities.Entities;
using Infra.Configs;
using Infra.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class NewsRepository : GenericRepository<News>, INews
    {
        private readonly DbContextOptions<Context> _OptionsBuilder;
        public NewsRepository()
        {
            _OptionsBuilder = new DbContextOptions<Context>();
        }

        public async Task<List<News>> GetAllNews(Expression<Func<News, bool>> exNews)
        {
            using (var data = new Context(_OptionsBuilder))
            {
                return await data.News.Where(exNews).AsNoTracking().ToListAsync();
            }
        }
    }
}
