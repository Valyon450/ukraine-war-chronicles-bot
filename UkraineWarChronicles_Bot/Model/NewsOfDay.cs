namespace UkraineWarChronicles_Bot.Model
{
    public class NewsOfDay
    {
        public Article article { get; set; }
        
        public string GetTitle()
        {
            Article article = this.article;
            return article.title;
        }

        public string GetText()
        {
            Article article = this.article;
            return article.text;
        }
    }

    public class Article
    {
        public string title { get; set; }
        public string text { get; set; }
    }
}
