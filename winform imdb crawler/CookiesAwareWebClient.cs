using System.Net;

namespace winform_imdb_crawler
{
    public class CookiesAwareWebClient : WebClient
    {
        private CookieContainer outboundCookies = new CookieContainer();
        private CookieCollection inboundCookies = new CookieCollection();

        public CookieContainer OutboundCookies
        {
            get { return outboundCookies; }
        }

        public CookieCollection InboundCookies
        {
            get { return inboundCookies; }
        }

        public bool IgnoreRedirects { get; set; }

        protected override WebRequest GetWebRequest(System.Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = outboundCookies;
                (request as HttpWebRequest).AllowAutoRedirect = !IgnoreRedirects;
                (request as HttpWebRequest).UserAgent =
                    "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                (request as HttpWebRequest).ContentType = "application/x-www-form-urlencoded";
            }
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            if (response is HttpWebResponse)
            {
                inboundCookies = (response as HttpWebResponse).Cookies ?? inboundCookies;
            }
            return response;
        }
  
    }
}
