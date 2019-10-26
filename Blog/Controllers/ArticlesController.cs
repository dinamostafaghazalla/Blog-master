using Blog.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Blog.Services.Interfaces.Services.Article;
using Microsoft.AspNetCore.Cors;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("corsPolicy")]
    public class ArticlesController : ControllerBase
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IArticleService _articleService;

        public ArticlesController(ILogger<ArticlesController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _articleService.GetAll();
            if (result.Result != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Insert(ArticleDto articleDto)
        {
            var result = _articleService.Insert(articleDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update(ArticleDto articleDto)
        {
            var result = _articleService.Edit(articleDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var returnData = _articleService.GetById(id);
                switch (returnData.Statuscode)
                {
                    case 200:
                        return Ok(returnData);
                    case 404:
                        return NotFound(returnData);
                    default:
                        return BadRequest(returnData);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _articleService.Delete(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}