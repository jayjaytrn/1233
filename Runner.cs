using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ScrapMe_
{
    class Runner
    {
        HtmlParser parser = new HtmlParser();
        public DataModel GetLinksAndTitle(Config config)
        {
            DataModel model = new DataModel();

            var responseStream = Response(config);
            var responseString = StreamToStringConvert(responseStream);
            var title = parser.GetHtmlTitle(responseString);
            var links = parser.GetAllLinks(responseString);
            Console.WriteLine(title);
            Console.ReadLine();
            model.HtmlTitle = title;
            model.LinksOnSite = links;
            model.SiteName = config.SiteToScrap;

            return model;
        }
        private Stream Response(Config config) 
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(config.SiteToScrap);
            var responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
            return responseStream;
        }
        private String StreamToStringConvert(Stream responseStream)
        {
            String responseString;
            using (Stream str = responseStream)
            {
                StreamReader reader = new StreamReader(str, Encoding.UTF8);
                responseString = reader.ReadToEnd();
            }
            return responseString;
        }
    }
}
