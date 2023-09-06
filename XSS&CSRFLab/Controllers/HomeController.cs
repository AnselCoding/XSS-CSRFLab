using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Text.Encodings.Web;
using XSS_CSRFLab.Models;

namespace XSS_CSRFLab.Controllers
{
    public class HomeController : Controller
    {
        // 透過使用編碼器，可以將不信任的資料，轉換成安全的格式
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javascriptEncoder;
        private UrlEncoder _urlEncoder;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,HtmlEncoder htmlEncoder, JavaScriptEncoder javascriptEncoder, UrlEncoder urlEncoder)
        {
            _logger = logger;
            _htmlEncoder = htmlEncoder;
            _javascriptEncoder = javascriptEncoder;
            _urlEncoder = urlEncoder;
        }

        public IActionResult Index()
        {
            string html = "<h1>123，abc。SOS.<h1>";
            string js = "<script>alert(document.cookie)</script>";
            string url = "https://www.google.com/";
            ViewBag.html = _htmlEncoder.Encode(html);
            ViewBag.js = _javascriptEncoder.Encode(js);
            ViewBag.url = _urlEncoder.Encode(url);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}