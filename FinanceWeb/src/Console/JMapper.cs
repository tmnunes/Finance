using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleGetter
{
    #region Seasonality
    public class LastDataSeasonality
    {
        public string symbol { get; set; }
        public string week { get; set; }
        public string date { get; set; }
        public string close { get; set; }
    }

    public class DataSeasonality
    {
        public string symbol { get; set; }
        public LastDataSeasonality lastData { get; set; }
        public List<List<object>> chartArray { get; set; }
        public string annualDataTotal { get; set; }
        public List<List<object>> annualDataArray { get; set; }
        public int numHistoricalData { get; set; }
        public int numFutureData { get; set; }
        public string yearlyDataSlider { get; set; }
        public string html { get; set; }
        public bool success { get; set; }
    }

    #endregion

    #region Score

    public class DataScore
    {
        public string symbol { get; set; }
        public Symbols symbols { get; set; }
        public string html { get; set; }
        public bool success { get; set; }
    }

    public class Symbols
    {
        public Stocks stocks { get; set; }
    }

    public class Stocks
    {
        public object[][] chart { get; set; }
        public object groupings { get; set; }
        public Indicators indicators { get; set; }
        public int numHistoricalData { get; set; }
        public Lastdata lastData { get; set; }
        public object[] defaultOverlays { get; set; }
        public string[] defaultIndicators { get; set; }
        public Score[] scores { get; set; }
        public object[][] scoresChart { get; set; }
        public int[] scoreMedian { get; set; }
        public int scoreDateOffset { get; set; }
    }

    public class Indicators
    {
        public object[][] ADL { get; set; }
        public object[][] MACD { get; set; }
        public object[][] RSI14 { get; set; }
    }

    public class Lastdata
    {
        public string symbol { get; set; }
        public string date { get; set; }
        public string week { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string close { get; set; }
        public string low { get; set; }
        public string open { get; set; }
        public string high { get; set; }
        public string SMA8 { get; set; }
        public string SMA21 { get; set; }
        public string SMA25 { get; set; }
        public string SMA50 { get; set; }
        public string SMA200 { get; set; }
        public string SMA100 { get; set; }
        public string EMA8 { get; set; }
        public string EMA20 { get; set; }
        public string EMA50 { get; set; }
        public string EMA200 { get; set; }
        public string RSI14 { get; set; }
        public string MACD { get; set; }
        public string ADLVolume { get; set; }
        public string STDDEV25 { get; set; }
        public string STDDEV100 { get; set; }
    }

    public class Score
    {
        public string date { get; set; }
        public string score { get; set; }
        public string signal { get; set; }
    }
    #endregion

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Ask { get; set; }
        public string Bid { get; set; }
        public string Askreal { get; set; }
        public string Bidreal { get; set; }
        public string Previousclose { get; set; }
        public string Open { get; set; }
        public string Change { get; set; }
    }
}
