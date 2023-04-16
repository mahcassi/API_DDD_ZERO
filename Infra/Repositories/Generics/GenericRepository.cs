using Domain.Interfaces.Generics;
using Infra.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Generics
{
    public class GenericRepository<T> : IGenerics<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<Context> _OptionsBuilder;

        public GenericRepository()
        {
            _OptionsBuilder = new DbContextOptions<Context>();
        }
        public async Task Create(T Obj)
        {
            using (var data = new Context(_OptionsBuilder))
            {
                await data.Set<T>().AddAsync(Obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task Update(T Obj)
        {
            using (var data = new Context(_OptionsBuilder))
            {
                data.Set<T>().Update(Obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Obj)
        {
            using (var data = new Context(_OptionsBuilder))
            {
                data.Set<T>().Remove(Obj);
                await data.SaveChangesAsync();
            }
        }


        public async Task<T> FindbyId(int Id)
        {
            using (var data = new Context(_OptionsBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new Context(_OptionsBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }




        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
