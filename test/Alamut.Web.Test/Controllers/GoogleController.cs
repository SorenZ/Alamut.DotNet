using Alamut.Utilities.Google;
using Microsoft.AspNetCore.Mvc;


namespace Alamut.Web.Test.Controllers
{
    public class GoogleController : Controller
    {
        private readonly CaptchaService _captchaService;

        public GoogleController(CaptchaService chaptchaService)
        {
            _captchaService = chaptchaService;
        }

        public IActionResult Index()
        {
            return Content("index");
        }

        public IActionResult Captcha()
        {
            return View();
        }

        public IActionResult CaptchaDo()
        {
            var code = Request.Form["g-recaptcha-response"];

            var result = _captchaService.ValidateGoogleChaptcha(code);

            return Json(result.Result);
        }


    }
}
