using System;
using System.Collections.Generic;
using System.Linq;
using Alamut.Data.Structure;
using Alamut.Sample.DataDriven.Contracts;
using Alamut.Sample.DataDriven.ViewModels;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;


namespace Alamut.Sample.DataDriven.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private static readonly Random RandomNumber = new Random(100);

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
                Title = "article number " + RandomNumber.Next(),
                Tags = new [] { "new", "article"}
            };

            var result = _articleService.Create(newArticle);

            return Json(result);
        }

        public IActionResult IdTitles1()
        {
            var articles = _articleService.ReadOnly.Queryable
                .ProjectTo<IdTitle>()
                .ToList();

            return Json(articles);
        }

        public IActionResult IdTitles2()
        {
            var articles = _articleService.ReadOnly
                .GetAll(s => new IdTitle
                {
                    Id = s.Id,
                    Title = s.Title
                });

            return Json(articles);
        }
    }
}
