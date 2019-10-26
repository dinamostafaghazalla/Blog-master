#region Usings

using AutoMapper;
using Blog.Models;
using Blog.Models.DTO;
using Blog.Repository.Interface;
using Blog.Repository.Interface.Repositories;
using Blog.Services.Interfaces.Services.Article;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Blog.Services.Services.Article
{
    public class ArticleService : BaseService<Models.Article, ArticleDto, int>, IArticleService
    {
        #region constructor

        public ArticleService(IRepository<Blog.Models.Article> repository, IMapper map, IUnitOfWork uow)
            : base(repository, uow, map)
        {
            _repository = repository;
            _map = map;
            _uow = uow;
        }

        #endregion

        #region intialization

        private readonly IRepository<Blog.Models.Article> _repository;
        private readonly IMapper _map;
        private readonly IUnitOfWork _uow;

        #endregion

        //GetAll
        public ReturnPagingDto<List<ArticleDto>> GetAll()
        {
            try
            {
                var articleList = _repository.GetAll().ToList();

                var pagedData = articleList.Select(c => new ArticleDto
                {
                    Id = c.Id,
                    Author = c.Author,
                    CreationDate = c.CreationDate,
                    Body = c.Body,
                    ImageUrl = c.ImageUrl,
                    Subtitle = c.Subtitle,
                    Title = c.Title
                });

                //Sort Order
                //if (!string.IsNullOrEmpty(sortField))
                //{
                //    pagedData = sortOrder == "desc"
                //        ? pagedData.OrderByDescending(c => c.GetType().GetProperty(sortField)?.GetValue(c)).ToList()
                //        : pagedData.OrderBy(c => c.GetType().GetProperty(sortField)?.GetValue(c)).ToList();
                //}

                ////Paging
                //if (pageSize != 0)
                //{
                //    pagedData = pagedData.Skip(pageNumber * pageSize).Take(pageSize).ToList();
                //}


                return new ReturnPagingDto<List<ArticleDto>>
                {
                    Result = _map.Map<List<ArticleDto>>(pagedData),
                    PageLength = 10
                };
            }
            catch (Exception ex)
            {
                return new ReturnPagingDto<List<ArticleDto>>();
            }
        }

        //Insert
        public ReturnModelDto<ArticleDto> Insert(ArticleDto blogPost)
        {
            var retrunObj = new ReturnModelDto<ArticleDto> {Statuscode = 400, Success = false};

            try
            {
                //var res = _map.Map<Models.Article>(blogPost);
                _repository.Create(new Models.Article()
                {
                    Author = blogPost.Author,
                    Title = blogPost.Title,
                    Subtitle = blogPost.Subtitle,
                    Body = blogPost.Body,
                    ImageUrl = blogPost.ImageUrl,
                    CreationDate = DateTime.UtcNow
                });
                _uow.Complete();

                retrunObj.Statuscode = 200;
                retrunObj.SucccessMessage = "Data inserted successfully";
                retrunObj.Success = true;
                retrunObj.Data = blogPost;
                return retrunObj;
            }

            catch (Exception ex)
            {
                retrunObj.Errors = new Errors[] {new Errors {Message = ex.Message}};
                retrunObj.Statuscode = 400;
                retrunObj.Success = false;
            }

            return retrunObj;
        }

        //GetDetail
        public ReturnModelDto<ArticleDto> GetById(int id)
        {
            var retrunObj = new ReturnModelDto<ArticleDto>();

            try
            {
                if (id == 0)
                {
                    retrunObj.Statuscode = 404;
                }

                else
                {
                    var blogPost = _repository.GetAll().Where(c => c.Id == id).Select(x => new ArticleDto
                    {
                        Id = x.Id,
                        Author = x.Author,
                        CreationDate = x.CreationDate,
                        Body = x.Body,
                        ImageUrl = x.ImageUrl,
                        Subtitle = x.Subtitle,
                        Title = x.Title
                    }).FirstOrDefault();
                    retrunObj.Data = blogPost;
                    retrunObj.Statuscode = 200;
                    retrunObj.Success = true;
                    retrunObj.SucccessMessage = "Data fetched successfully";
                }
            }
            catch (Exception ex)
            {
                retrunObj.Errors = new Errors[] {new Errors {Message = ex.Message}};
                retrunObj.Statuscode = 400;
                retrunObj.Success = false;
            }

            return retrunObj;
        }

        //Update
        public ReturnModelDto<ArticleDto> Edit(ArticleDto blogPost)
        {
            var retrunObj = new ReturnModelDto<ArticleDto> {Statuscode = 400, Success = false};

            try
            {
                if (blogPost.Id != 0)
                {
                    var article = _repository.GetAll().FirstOrDefault(x => x.Id == blogPost.Id);
                    if (article is null)
                    {
                        retrunObj.Errors = new [] { new Errors() { Message = "article not found!" } };
                        retrunObj.Success = false;
                        retrunObj.Statuscode = 404;
                        return retrunObj;
                    }
                    // update article
                    article.Author = blogPost.Author;
                    article.Title = blogPost.Title;
                    article.Subtitle = blogPost.Subtitle;
                    article.Body = blogPost.Body;
                    article.CreationDate = DateTime.UtcNow;
                    article.ImageUrl = blogPost.ImageUrl;
                    _uow.Complete();
                    retrunObj.Statuscode = 200;
                    retrunObj.SucccessMessage = "Data updated successfully";
                    retrunObj.Success = true;
                    return retrunObj;
                }

                throw new Exception("Data not valid");
            }

            catch (Exception ex)
            {
                retrunObj.Errors = new Errors[] {new Errors {Message = ex.Message}};
                retrunObj.Statuscode = 400;
                retrunObj.Success = false;
            }

            return retrunObj;
        }

        //Delete
        public ReturnModelDto<List<ArticleDto>> Delete(int id)
        {
            var retrunObj = new ReturnModelDto<List<ArticleDto>>();

            try
            {
                if (id is 0)
                {
                    throw new Exception("Please Support Blog Article id");
                }

                var itemDeleted = _repository.GetAll().FirstOrDefault(c => c.Id == id);
                if (itemDeleted != null)
                {
                    _repository.HardDelete(itemDeleted);
                    _uow.Complete();
                    retrunObj.Statuscode = 200;
                    retrunObj.Success = true;
                    retrunObj.SucccessMessage = "Data Deleted successfully";
                }

                else
                {
                    throw new Exception("Blog Article Not Found or has been delete before");
                }
            }
            catch (Exception ex)
            {
                retrunObj.Errors = new Errors[] {new Errors {Message = ex.Message}};
                retrunObj.Statuscode = 400;
                retrunObj.Success = false;
            }

            return retrunObj;
        }
    }
}