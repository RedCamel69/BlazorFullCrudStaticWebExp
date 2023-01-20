using BlazorEcommerceStaticWebApp.Shared;
using System.Net.NetworkInformation;
using System.ServiceModel.Syndication;
using System.Xml;

namespace BlazorEcommerceStaticWebApp.Client.Services.NewsService
{
    public class NewsService : INewsService
    {
        private SyndicationFeed _feed;
        private List<NewsItem> _newsItems = new List<NewsItem>();
        public List<NewsItem> Get()
        {
            try
            {
                using (var reader = XmlReader.Create("https://www.cbr.com/feed/category/comics/news/"))
                {
                    _feed = SyndicationFeed.Load(reader);

                    foreach (var item in _feed.Items)
                    {
                        _newsItems.Add(new NewsItem()
                        {
                            Title = item.Title.Text,
                            Link = item.Title.Text
                        });
                    }
                }

                return _newsItems;
            }
            catch(Exception ex)
            {
                return new List<NewsItem>();
            }
        }
    }
}
