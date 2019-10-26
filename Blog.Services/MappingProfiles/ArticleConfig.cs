#region Usings

using AutoMapper;
using Blog.Models;
using Blog.Models.DTO;

#endregion

namespace Blog.Services.MappingProfiles
{
    public class ArticleConfig : Profile
    {
        public ArticleConfig()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
        }
    }
}