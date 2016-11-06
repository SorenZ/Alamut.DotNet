using Alamut.Sample.DataDriven.Contracts;
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
    }
}
