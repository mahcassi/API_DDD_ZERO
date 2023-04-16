using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generics
{
    public interface IGenerics<T> where T : class
    {

        Task Create(T obj);
        Task<T> Update(T obj);
        Task<T> Delete(T obj);
        Task<T> FindbyId(int id);
        Task<List<T>> List();
    }
}
