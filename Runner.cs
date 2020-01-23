using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScrapMe_
{
    class Runner
    {
        HtmlParser parser = new HtmlParser();
        List<string> TitlesBank = new List<string>();
        byte depth = 10;
        byte count = 0;
        public void Result()
        {
            var titles = TitlesBank;
            (from t in titles.AsParallel()
             select t)
            .ForAll(Console.WriteLine);
            Console.ReadLine();
        }
        public void RunCircle(HashSet<string> hashSetLinks)
        {
            while(count <= depth)
            {
                var hashSetLinksListForOneCircle = hashSetLinks.AsParallel()
                    .Select(site => GetLinksAndAddTitle(site))
                    .Aggregate((megaset, set) => { 
                        megaset.UnionWith(set); return megaset; }).ToHashSet();
                count++;
                RunCircle(hashSetLinksListForOneCircle);
            }
        }
        private HashSet<string> GetLinksAndAddTitle(string site)
        {
            try 
            { 
                var responseStream = Response(site);
                var responseString = StreamToStringConvert(responseStream);
                var title = parser.GetHtmlTitle(responseString);
                TitlesBank.Add(title);
                var links = parser.GetAllLinks(responseString);
                links.RemoveAll(l => !l.Contains("http"));
                HashSet<string> hashSetLinks = new HashSet<string>(links);
                return hashSetLinks;
            }
            catch (Exception e)
            {
                return new HashSet<string>();
            }
            
        }
        private Stream Response(string site)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(site);
            Stream responseStream;
            try
            {
                responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
            }
            catch(Exception e)
            {
                return null;
            }
            return responseStream;
        }
        private String StreamToStringConvert(Stream responseStream)
        {
            String responseString;
            try
            {
                using (Stream str = responseStream)
                {
                    StreamReader reader = new StreamReader(str, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return responseString;
        }
    }
}
