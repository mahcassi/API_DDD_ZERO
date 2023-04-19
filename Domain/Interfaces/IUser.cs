using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUser
    {

        Task<bool> CreateUser(string email, string password, int age, string cellphone);

        Task<bool> ExistUser(string email, string password);
    }
}
