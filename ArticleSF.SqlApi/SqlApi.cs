namespace ArticleSF.SqlApi
{
    using ArticleSF.Business.Article;
    using ArticleSF.Business.Article.Store;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;
    using System.Collections.Generic;
    using System.Fabric;
    using System.Threading.Tasks;

    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class SqlApi : StatelessService, IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public SqlApi(StatelessServiceContext context)
            : base(context)
        {
            _articleRepository = new ArticleStore();
        }

        public async Task<int> AddArticle(Article article)
        {
            return await _articleRepository.AddArticle(article);
        }

        public async Task<bool> DeleteArticle(int articleId)
        {
            return await _articleRepository.DeleteArticle(articleId);
        }

        public async Task<Article> GetArticleById(int articleId)
        {
            return await _articleRepository.GetArticleById(articleId);
        }

        public async Task<Article[]> GetArticles()
        {
            return await _articleRepository.GetArticles();
        }

        public async Task<int> UpdateArticle(Article article)
        {
            return await _articleRepository.UpdateArticle(article);
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }
    }
}
