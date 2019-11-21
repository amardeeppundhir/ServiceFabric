namespace ArticleSF.WebApi.Controllers
{
    using ArticleSF.Business.Article;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.ServiceFabric.Services.Client;
    using Microsoft.ServiceFabric.Services.Remoting.Client;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
    using System;
    using System.Threading.Tasks;

    [Route("webapi/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController()
        {
            _articleService = ServiceProxy.Create<IArticleService>(new Uri("fabric:/ArticleSF/ArticleSF.WebApi"));
        }

        // GET: api/Article
        [HttpGet]
        public async Task<Article[]> Get()
        {
            return await _articleService.GetArticles();
        }

        // GET: api/Article/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Article> Get(int id)
        {
            return await _articleService.GetArticleById(id);
        }

        // POST: api/Article
        [HttpPost]
        public async Task<int> Post([FromBody] Article article)
        {
            return await _articleService.AddArticle(article);
        }

        // PUT: api/Article/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] Article article)
        {
            return await _articleService.UpdateArticle(article);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _articleService.DeleteArticle(id);
        }
    }
}
