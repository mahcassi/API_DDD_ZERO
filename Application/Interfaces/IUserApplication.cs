using Application.Interfaces.Generics;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserApplication 
    {
        Task<bool> CreateUser(string email, string password, int age, string cellphone);
        Task<bool> ExistUser(string email, string password);
    }
}
