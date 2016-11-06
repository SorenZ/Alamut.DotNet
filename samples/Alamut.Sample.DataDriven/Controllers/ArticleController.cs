using System.Collections.Generic;
using Alamut.Sample.DataDriven.Contracts;
using Alamut.Sample.DataDriven.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Alamut.Sample.DataDriven.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            var articles = _articleService.ReadOnly.GetAll();

            return Json(articles);
        }

        public IActionResult Create()
        {
            var newArticle = new ArticleCreateVm
            {
                Title = "new article ",
                Tags = new [] { "new", "article"}
            };

            var result = _articleService.Create(newArticle);

            return Json(result);
        }
    }
}
