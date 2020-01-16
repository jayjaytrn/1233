using System;
using System.Net;

namespace ScrapMe_
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = new Config();
            config.SiteToScrap = "https://zen.yandex.ru/";
            config.Depth = 1;

            Runner runner = new Runner();
            runner.GetLinksAndTitle(config);
        }
    }
}
