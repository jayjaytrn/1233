using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapMe_
{
    class DataModel
    {
        public string SiteName { get; set; }
        public string HtmlTitle { get; set; }
        public List<string> LinksOnSite { get; set; }
    }
}
