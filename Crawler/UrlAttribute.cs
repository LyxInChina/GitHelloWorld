using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class UrlAttribute : Attribute
    {
        public string Address { get; set; }
        public CrawlerMode Mode { get; set; }
        public UrlAttribute(string url, CrawlerMode mode)
        {
            Address = url;
            Mode = mode;
        }
    }
    public enum CrawlerMode
    {
        AllIndex,
        OnePage,
    }
}
