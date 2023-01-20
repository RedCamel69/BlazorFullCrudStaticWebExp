using BlazorEcommerceStaticWebApp.Shared;

namespace BlazorEcommerceStaticWebApp.Client.Services.NewsService
{
    public interface INewsService
    {
        public List<NewsItem> Get();

    }
}
