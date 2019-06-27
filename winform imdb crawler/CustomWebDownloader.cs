using System.Net;
using NCrawler.Services;

namespace winform_imdb_crawler
{
    public class CustomWebDownloader : WebDownloaderV2
    {
        private readonly CookieContainer _cookieContainer;

        public CustomWebDownloader(CookieContainer cookieContainer)
        {
            _cookieContainer = cookieContainer;
        }

        protected override void SetDefaultRequestProperties(HttpWebRequest request)
        {
            base.SetDefaultRequestProperties(request);
            request.CookieContainer = _cookieContainer;
        }
    }
}