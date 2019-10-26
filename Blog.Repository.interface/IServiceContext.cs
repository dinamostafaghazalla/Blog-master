#region Usings
using Blog.Models;
using Microsoft.EntityFrameworkCore;
#endregion


namespace Blog.Repository.Interface
{
    public interface IServiceContext
    {
        DbSet<Article> Articles { get; set; }
    }
}