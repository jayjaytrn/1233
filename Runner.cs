using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ScrapMe_
{
    class Runner
    {
        HtmlParser parser = new HtmlParser();
        List<string> TitlesBank = new List<string>();
        byte depth = 1;
        byte count = 0;
        public void Result()
        {
            foreach (string title in TitlesBank)
            {
                Console.WriteLine(title);
            }
            Console.ReadLine();
        }
        public void RunCircle(HashSet<string> hashSetLinks)
        {
            while(count <= depth)
            {
                HashSet<string> hashSetLinsListForOneCircle = new HashSet<string>();
                foreach (string link in hashSetLinks)
                {
                    HashSet<string> hashSet;
                    try
                    {
                        hashSet = GetLinksAndAddTitles(link);
                        hashSetLinsListForOneCircle.UnionWith(hashSet);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                count++;
                RunCircle(hashSetLinsListForOneCircle);
            }
        }
        private HashSet<string> GetLinksAndAddTitles(string link)
        {

            try 
            { 
                var responseStream = Response(link);
                var responseString = StreamToStringConvert(responseStream);
                var title = parser.GetHtmlTitle(responseString);
                TitlesBank.Add(title);
                var links = parser.GetAllLinks(responseString);
                links.RemoveAll(l => !l.Contains("http"));
                HashSet<string> hashSetLinks = new HashSet<string>(links);
                return hashSetLinks;
            }
            catch (Exception)
            {
                return null;
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
            catch
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
