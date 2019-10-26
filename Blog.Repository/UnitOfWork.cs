#region Usings

using System;
using System.Threading.Tasks;
using Blog.Repository.Interface;

#endregion

namespace Blog.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ServiceContext _ctx;

        public UnitOfWork(ServiceContext ctx)
        {
            _ctx = ctx;
        }


        public int Complete()
        {
            return _ctx.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _ctx.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
                _ctx?.Dispose();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}