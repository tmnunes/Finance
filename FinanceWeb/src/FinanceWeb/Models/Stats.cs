using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceWeb.Models
{
    public class Stats
    {
        public long AnnualCount { get; set; }
        public long FutureCount { get; set; }
        public long PastCount { get; set; }
        public long RealCount { get; set; }
        public long StockDataCount { get; set; }
        public long ScoreDataCount { get; set; }
        public long SeasonalityDataCount { get; set; }
        public long StockCount { get; set; }
    }
}
