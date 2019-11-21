namespace ArticleSF.Web.Controllers
{
    using ArticleSF.Business.Article;
    using ArticleSF.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.ServiceFabric.Services.Remoting.Client;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            _articleService = ServiceProxy.Create<IArticleService>(new Uri("fabric:/ArticleSF/ArticleSF.WebApi"));

        }

        public async Task<IActionResult> Index()
        {
            return View(await _articleService.GetArticles());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article =await _articleService.GetArticleById(id.Value);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Title,Body")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.PublishDate = DateTime.Now;
                var result =await _articleService.AddArticle(article);

                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article =await _articleService.GetArticleById(id.Value);

            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,CategoryId,Title,Body")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    article.PublishDate = DateTime.UtcNow;
                    var result =await _articleService.UpdateArticle(article);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result =await _articleService.GetArticleById(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article =await _articleService.GetArticleById(id);

            await _articleService.DeleteArticle(id);

            return RedirectToAction(nameof(Index));
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
