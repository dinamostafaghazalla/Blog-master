using System.Collections.Generic;
using Blog.Models.DTO;
using BlogPost.Services.Interfaces.Services;

namespace Blog.Services.Interfaces.Services.Article
{
    public interface IArticleService : IBaseService<Models.Article, ArticleDto>
    {
        ReturnPagingDto<List<ArticleDto>> GetAll();
        ReturnModelDto<ArticleDto> Insert(ArticleDto blogPost);
        ReturnModelDto<ArticleDto> GetById(int id);
        ReturnModelDto<ArticleDto> Edit(ArticleDto blogPost);
        ReturnModelDto<List<ArticleDto>> Delete(int id);
    }
}