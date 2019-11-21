namespace ArticleSF.WebApi
{
    using ArticleSF.Business.Article;
    using Microsoft.ServiceFabric.Services.Client;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.Client;
    using Microsoft.ServiceFabric.Services.Remoting.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;
    using System;
    using System.Collections.Generic;
    using System.Fabric;
    using System.Threading.Tasks;

    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class WebApi : StatelessService, IArticleService
    {
        private readonly IArticleService _articleService;

        public WebApi(StatelessServiceContext context)
            : base(context)
        {

            _articleService = ServiceProxy.Create<IArticleService>(new Uri("fabric:/ArticleSF/ArticleSF.SqlApi"));

            //var proxyFactory = new ServiceProxyFactory(
            //    c => new FabricTransportServiceRemotingClientFactory());

            //_articleService = proxyFactory.CreateServiceProxy<IArticleService>(
            //    new Uri("fabric:/ArticleSF/ArticleSF.SqlApi"),
            //    new ServicePartitionKey(0));
        }

        public async Task<int> AddArticle(Article article)
        {
            return await _articleService.AddArticle(article);
        }

        public async Task<bool> DeleteArticle(int articleId)
        {
            return await _articleService.DeleteArticle(articleId);
        }

        public async Task<Article> GetArticleById(int articleId)
        {
            return await _articleService.GetArticleById(articleId);
        }

        public async Task<Article[]> GetArticles()
        {
            return await _articleService.GetArticles();
        }

        public async Task<int> UpdateArticle(Article article)
        {
            return await _articleService.UpdateArticle(article);
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
