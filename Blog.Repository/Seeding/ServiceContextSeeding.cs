using System;
using Blog.Models;

namespace Blog.Repository.Seeding
{
    public interface ISeedDataClass
    {
        void Seed();
    }

    public class ServiceContextSeeding : ISeedDataClass
    {
        private readonly ServiceContext _context;

        public ServiceContextSeeding(ServiceContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Articles.AddRange(
                new Article
                {
                    //Id = 1,
                    Author = "Saber",
                    Body = "Saber",
                    CreationDate = DateTime.UtcNow,
                    ImageUrl = "Saber",
                    Subtitle = "Saber",
                    Title = "Saber"
                });
        }
    }
}