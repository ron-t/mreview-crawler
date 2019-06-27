using CsvHelper.Configuration;

namespace winform_imdb_crawler
{

    partial class User
    {
        public string _member_since_utc { get; set; }
        public string _total_ratings { get; set; }

    }

    public sealed class UserMap : CsvClassMap<User>
    {
        public UserMap()
        {
            Map(m => m.Username).Name(@"name");
            Map(m => m.RatingsAnalysisRaw).Name(@"ratings_analysis");

            //Map(m => m._member_since_utc).Name(@"member_since").Default(-1.0);
            Map(m => m._member_since_utc).Name(@"member_since/_source");
            Map(m => m.PageUrl).Name(@"pageUrl");
            Map(m => m._total_ratings).Name(@"total_ratings");
        }
    }
}
