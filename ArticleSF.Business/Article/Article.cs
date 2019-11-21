namespace ArticleSF.Business.Article
{
    using System;

    public class Article
    {
        public int ArticleId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
