using System;
using NLog;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using FinanceWeb.Models;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Getter
{
    public class OutsideCom
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Requests the financhill.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static async Task<StockSeasonalityData> RequestFinanchill(StockSymbol args)
        {
            try
            {
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create("https://financhill.com/fetchRemainingData?_framework_ajax=1");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string postData = "section=seasonality&navSection=search&symbol=" + args.symbol + "&urlData%5B%5D=search&urlData%5B%5D=seasonality&urlData%5B%5D=" + args.symbol;

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(postData), 0, postData.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                StockSeasonalityData SSData = new StockSeasonalityData();
                SSData._id = ObjectId.GenerateNewId();
                SSData.guid = Guid.NewGuid().ToString();
                SSData.datetime = DateTime.Now;
                SSData.stock = args.symbol;
                SSData.jsondata = content;

                return SSData;
            }
            catch (Exception e)
            {
                _logger.Fatal("Request Financhill Seasonality Exception: " + e);
                Console.WriteLine("Request Financhill Seasonality Exception: " + e);
                return null;
            }
        }

        /// <summary>
        /// Requests the financhill score.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static async Task<StockScoreData> RequestFinanchillScore(StockSymbol args)
        {
            try
            {
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create("https://financhill.com/loadSection?_framework_ajax=1");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string postData = "section=stock-score&navSection=search&symbol=" + args.symbol + "&urlData%5B%5D=search&urlData%5B%5D=stock-score&urlData%5B%5D=" + args.symbol;

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(postData), 0, postData.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                StockScoreData SSData = new StockScoreData();
                SSData._id = ObjectId.GenerateNewId();
                SSData.guid = Guid.NewGuid().ToString();
                SSData.datetime = DateTime.Now;
                SSData.stock = args.symbol;
                SSData.jsondata = content;

                return SSData;
            }
            catch (Exception e)
            {
                _logger.Fatal("Request Financhill Score Exception: " + e);
                Console.WriteLine("Request Financhill Score Exception: " + e);
                return null;
            }
        }

        /// <summary>
        /// Gets the data g finance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static async Task<StockData> GetDataGFinance(StockSymbol args)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://download.finance.yahoo.com/d/quotes.csv?s=" + args.symbol + "&f=snabb2b3poc1");
                request.Method = "GET";

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string[] contentsplit;
                string content = reader.ReadToEnd();
                content = content.Replace("\"", "");
                content = content.Replace(", Inc", " Inc");
                contentsplit = content.Split('\n');

                Stock stock = new Stock();
                foreach (var contents in contentsplit)
                {
                    if (contents != "")
                    {
                        var lines = contents.Split(',');
                        stock.Symbol = lines[0];
                        stock.Name = lines[1];
                        stock.Ask = lines[2];
                        stock.Bid = lines[3];
                        stock.Askreal = lines[4];
                        stock.Bidreal = lines[5];
                        stock.Previousclose = lines[6];
                        stock.Open = lines[7];
                        stock.Change = lines[8];
                    }
                }

                StockData SData = new StockData();
                SData._id = ObjectId.GenerateNewId();
                SData.guid = Guid.NewGuid().ToString();
                SData.datetime = DateTime.Now;
                SData.stock = args.symbol;
                SData.jsondata = JsonConvert.SerializeObject(stock);

                return SData;
            }
            catch (Exception e)
            {
                _logger.Fatal("Request Financhill Score Exception: " + e);
                Console.WriteLine("Request Financhill Score Exception: " + e);
                return null;
            }
        }


    }
}
