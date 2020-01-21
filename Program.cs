using System;
using System.Collections.Generic;
using System.Net;

namespace ScrapMe_
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner runner = new Runner();
            HashSet<string> firstSite = new HashSet<string>();
            firstSite.Add("https://zen.yandex.ru/");
            runner.RunCircle(firstSite);
            runner.Result();
        }
    }
}
