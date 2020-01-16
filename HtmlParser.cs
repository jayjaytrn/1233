using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace ScrapMe_
{
    class HtmlParser
    {
        public string GetHtmlTitle(string html)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);
            var title = htmlSnippet.DocumentNode.SelectSingleNode("//head/title").InnerText;
            return title;
        }
        public List<string> GetAllLinks(string html)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);

            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }
    }
}