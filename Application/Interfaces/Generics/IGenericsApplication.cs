using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Generics
{
    public interface IGenericsApplication<T> where T : class
    {
        Task Create(T Obj);
        Task Update(T Obj);
        Task Delete(T Obj);
        Task<T> FindbyId(int Id);
        Task<List<T>> List();
    }
}
