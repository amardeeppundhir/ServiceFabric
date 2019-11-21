namespace ArticleSF.Business.Article
{
    using Microsoft.ServiceFabric.Services.Remoting;
    using System.Threading.Tasks;

    public interface IArticleService : IService
    {
        Task<Article[]> GetArticles();

        Task<Article> GetArticleById(int articleId);

        Task<int> AddArticle(Article article);

        Task<int> UpdateArticle(Article article);

        Task<bool> DeleteArticle(int articleId);
    }
}
