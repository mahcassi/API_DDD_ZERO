using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generics
{
    public interface IGenerics<T> where T : class
    {

        Task Create(T Obj);
        Task Update(T Obj);
        Task Delete(T Obj);
        Task<T> FindbyId(int Id);
        Task<List<T>> List();
    }
}
