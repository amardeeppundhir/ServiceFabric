namespace ArticleSF.SqlApi.Controllers
{
    using ArticleSF.Business.Article;
    using ArticleSF.Business.Article.Store;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("sqlapi/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = new ArticleStore();
        }

        // GET: api/Article
        [HttpGet]
        public async Task<Article[]> Get()
        {
            return await _articleRepository.GetArticles();
        }

        // GET: api/Article/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Article> Get(int id)
        {
            return await _articleRepository.GetArticleById(id);
        }

        // POST: api/Article
        [HttpPost]
        public async Task<int> Post([FromBody] Article article)
        {
            return await _articleRepository.AddArticle(article);
        }

        // PUT: api/Article/5
        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] Article article)
        {
            return await _articleRepository.UpdateArticle(article);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _articleRepository.DeleteArticle(id);
        }
    }
}
