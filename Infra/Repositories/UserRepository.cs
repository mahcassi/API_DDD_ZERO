using Domain.Interfaces;
using Entities.Entities;
using Infra.Configs;
using Infra.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUser
    {
        private readonly DbContextOptions<Context> _OptionsBuilder;

        public UserRepository()
        {
            _OptionsBuilder = new DbContextOptions<Context>();
        }
        public async Task<bool> CreateUser(string email, string password, int age, string cellphone)
        {
            try
            {
                using (var data = new Context(_OptionsBuilder))
                {
                    await data.ApplicationUser.AddAsync(
                        new ApplicationUser { 
                            Email = email,
                            PasswordHash = password,
                            Age = age,
                            Cellphone = cellphone
                        });

                    await data.SaveChangesAsync();

                }
            } 
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
