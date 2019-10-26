#region Usings

using System;
using System.Threading.Tasks;

#endregion

namespace Blog.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        Task<int> CompleteAsync();
    }
}