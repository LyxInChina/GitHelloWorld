using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    public struct CrawlerConfig
    {
        public string MUrl { get; set; }
        public AnalyzePageFn MPageFn { get; set; }
        public AnalyzeUrlsFn MUrlsFn { get; set; }
        public string Name { get; set; }
    }
}
