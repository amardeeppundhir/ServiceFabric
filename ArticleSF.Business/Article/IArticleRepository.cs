namespace ArticleSF.Business.Article
{
    using System.Threading.Tasks;

    public interface IArticleRepository
    {
        Task<Article[]> GetArticles();

        Task<Article> GetArticleById(int articleId);

        Task<int> AddArticle(Article article);

        Task<int> UpdateArticle(Article article);

        Task<bool> DeleteArticle(int articleId);
    }
}
