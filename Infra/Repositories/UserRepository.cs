using Domain.Interfaces;
using Entities.Entities;
using Entities.Enums;
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
                            Cellphone = cellphone,
                            UserType = EUserType.Common
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

        public async Task<bool> ExistUser(string email, string password)
        {
            try
            {
                using(var data = new Context(_OptionsBuilder))
                {
                    return await data.ApplicationUser
                        .Where(u => u.Email.Equals(email) && u.PasswordHash.Equals(password))
                        .AsNoTracking()
                        .AnyAsync();
                }
            }   catch(Exception)
            {
                return false;
            }
        }
    }
}
