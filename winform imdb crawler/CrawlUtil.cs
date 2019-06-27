using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winform_imdb_crawler
{
    class CrawlUtil
    {
        public static string getQueryValueFromUrl(string url, string key)
        {
            return System.Web.HttpUtility.ParseQueryString(url)[key];
        }

        public static string getMovieNameFromTitle(string title)
        {
            try
            {
                title = title.Split(new string[] { "Reviews" }, StringSplitOptions.None)[0].Trim();
            } 
            catch
            {
                title = "NoTitle";
            }

            return title;
        }

        public static string SanitiseFileName(string input)
        {
            var invalids = System.IO.Path.GetInvalidFileNameChars();
            var newName = String.Join("_", input.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');

            return newName;
        }
    }
}
