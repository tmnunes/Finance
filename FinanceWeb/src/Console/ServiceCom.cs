using System;
using NLog;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using FinanceWeb.Models;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ConsoleGetter
{
    public class ServiceCom
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The URL stock
        /// </summary>
        private const string UrlStock = "http://localhost:83/api/system";
        /// <summary>
        /// The URL seasonality
        /// </summary>
        private const string UrlSeasonality = "http://localhost:83/api/Seasonality";
        /// <summary>
        /// The URL score
        /// </summary>
        private const string UrlScore = "http://localhost:83/api/Score";
        /// <summary>
        /// The URL stock yahoo
        /// </summary>
        private const string UrlStockYahoo = "http://localhost:83/api/Stock";
        /// <summary>
        /// The URL real data
        /// </summary>
        private const string UrlRealData = "http://localhost:83/api/RealData";
        /// <summary>
        /// The URL login
        /// </summary>
        private const string UrlLogin = "http://localhost:83/Account/Login";
        /// <summary>
        /// The URL annual
        /// </summary>
        private const string UrlAnnual = "http://localhost:83/api/AnnualData";

        private const string UrlFuture = "http://localhost:83/api/FutureData";
        private const string UrlPast = "http://localhost:83/api/PastData";
        private const string UrlLast = "http://localhost:83/api/LastData";
        private const string UrlExecution = "http://localhost:83/api/Execution";

        /// <summary>
        /// The stocks
        /// </summary>
        public static List<StockSymbol> _stocks = new List<StockSymbol>();
        /// <summary>
        /// The stocks aux
        /// </summary>
        public static List<StockSymbol> _stocks_aux = new List<StockSymbol>();

        /// <summary>
        /// The stock
        /// </summary>
        static List<string> stock = new List<string>(
            new string[]
            {"AAPL"
                ,"ACN",
                "GOOGL",
                "AMZN",
                "CME",
                "HSIC",
                "MU",
                "PANW",
                "CRM",
                "SBUX",
                "NVEE",
                "MBLY",
                "INGN",
                "GM",
                "OGS",
                "USG",
                "IRT",
                "DY",
                "IBM",
                "PAYC"
            });

        #region StockSymbol

        /// <summary>
        /// Loads the stocks.
        /// </summary>
        /// <returns></returns>
        public static async Task LoadStocks()
        {
            string content = "";
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlStock);
                request.Method = "GET";
                request.ContentType = "application/json";

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                content = reader.ReadToEnd();

                object objResponse = JsonConvert.DeserializeObject<List<StockSymbol>>(content);

                List<StockSymbol> collection = (List<StockSymbol>)objResponse;

                foreach (StockSymbol obj in collection)
                {
                    if (stock.Exists(p => p == obj.symbol) || stock.Count == 0)
                        _stocks.Add(obj);
                }

                _stocks_aux = _stocks;
            }
            catch (Exception e)
            {
                _logger.Fatal("Load Stock Content: " + content + "|| Exception: " + e);
                Console.WriteLine("Load Stock Content: " + content + "|| Exception: " + e);
            }
        }

        #endregion

        #region Seasonality

        /// <summary>
        /// Inserts the seasonality.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task InsertSeasonality(StockSeasonalityData data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlSeasonality);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");
                request.UseDefaultCredentials = true;

                string jdata = JsonConvert.SerializeObject(data);

                //var logPath = System.IO.Path.GetTempFileName();
                //var logFile = System.IO.File.Create(logPath);
                //var logWriter = new System.IO.StreamWriter(logFile);
                //logWriter.WriteLine(jdata);
                //logWriter.Dispose();

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                //Console.WriteLine(content);
                _logger.Debug("Insert Seasonality Data " + data.stock + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Seasonality " + data.stock + " Exception: " + e);
                Console.WriteLine("Insert Seasonality " + data.stock + " Exception: " + e);
            }
        }

        /// <summary>
        /// Gets the first seasonality.
        /// </summary>
        /// <returns></returns>
        public static async Task<StockSeasonalityData> GetFirstSeasonality()
        {
            string content = "";
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlSeasonality);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                content = reader.ReadToEnd();

                StockSeasonalityData objResponse = JsonConvert.DeserializeObject<StockSeasonalityData>(content);

                if (objResponse == null)
                {
                    return null;
                }

                _logger.Debug("Get Seasonality Data " + objResponse.stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get Seasonality Data Content:" + content + "|| Exception: " + e);
                Console.WriteLine("Get Seasonality Data Content:" + content + "|| Exception: " + e);
                return null;
            }
        }

        public static async Task<string> DeleteSeasonalityData(string id)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlSeasonality + "/" + id);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Delete Seasonality Data " + ": " + "OK");
                return "OK";
            }
            catch (Exception e)
            {
                _logger.Fatal("Delete Seasonality Data " + " Exception: " + e);
                Console.WriteLine("Delete Seasonality Data " + " Exception: " + e);
                return null;
            }
        }

        #endregion

        #region Score

        /// <summary>
        /// Inserts the score data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task InsertScoreData(StockScoreData data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlScore);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                _logger.Debug("Insert Score Data " + data.stock + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Score Data " + data.stock + " Exception: " + e);
                Console.WriteLine("Insert Score " + data.stock + " Data Exception: " + e);
            }
        }

        /// <summary>
        /// Gets the first score.
        /// </summary>
        /// <returns></returns>
        public static async Task<StockScoreData> GetFirstScore()
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlScore);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                StockScoreData objResponse = JsonConvert.DeserializeObject<StockScoreData>(content);

                if (objResponse == null)
                {
                    _logger.Fatal("Get First Score Data " + " Content: " + content);
                    Console.WriteLine("Get First Score Data " + " Content: " + content);
                    return null;
                }

                objResponse.jsondata = objResponse.jsondata.Replace("\"" + objResponse.stock + "\":{", "\"stocks\":{");
                objResponse.jsondata = objResponse.jsondata.Replace(objResponse.stock + " had", "stock");

                _logger.Debug("Get Score Data " + objResponse.stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get Score Data " + " Exception: " + e);
                Console.WriteLine("Get Score Data " + " Exception: " + e);
                return null;
            }
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns></returns>
        public static async Task<StockScoreData> GetScore(string stock)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlScore + "/" + stock);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                StockScoreData objResponse = JsonConvert.DeserializeObject<StockScoreData>(content);

                if (objResponse == null)
                {
                    _logger.Fatal("Get Score Data " + stock + " Content: " + content);
                    Console.WriteLine("Get Score Data " + stock + " Content: " + content);
                    return null;
                }

                objResponse.jsondata = objResponse.jsondata.Replace("\"" + objResponse.stock + "\":{", "\"stocks\":{");
                objResponse.jsondata = objResponse.jsondata.Replace(objResponse.stock + " had", "stock");

                //DataScore objResponseaux = JsonConvert.DeserializeObject<DataScore>(objResponse.jsondata);
                _logger.Debug("Get Score Data " + objResponse.stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get Score Data " + " Exception: " + e);
                Console.WriteLine("Get Score Data " + " Exception: " + e);
                return null;
            }
        }

        public static async Task<StockScoreData> GetScorebyDate(string stock, string date)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlScore + "/" + stock + "/" + date);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                StockScoreData objResponse = JsonConvert.DeserializeObject<StockScoreData>(content);

                if (objResponse == null)
                {
                    _logger.Fatal("Get Score Data Date" + stock + date + " Content: " + content);
                    Console.WriteLine("Get Score Data Date " + stock + date + " Content: " + content);
                    return null;
                }

                objResponse.jsondata = objResponse.jsondata.Replace("\"" + objResponse.stock + "\":{", "\"stocks\":{");
                objResponse.jsondata = objResponse.jsondata.Replace(objResponse.stock + " had", "stock");

                _logger.Debug("Get Score Data by date " + objResponse.stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get Score Data by date " + " Exception: " + e);
                Console.WriteLine("Get Score Data by date " + " Exception: " + e);
                return null;
            }
        }

        public static async Task<string> DeleteScoreData(string id)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlScore + "/" + id);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Delete Score Data " + ": " + "OK");
                return "OK";
            }
            catch (Exception e)
            {
                _logger.Fatal("Delete Score Data " + " Exception: " + e);
                Console.WriteLine("Delete Score Data " + " Exception: " + e);
                return null;
            }
        }

        #endregion

        #region StockData

        /// <summary>
        /// Gets the first stock data.
        /// </summary>
        /// <returns></returns>
        public static async Task<StockData> GetFirstStockData()
        {
            string content = "";
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlStockYahoo);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                content = reader.ReadToEnd();

                StockData objResponse = JsonConvert.DeserializeObject<StockData>(content);

                if (objResponse == null)
                {
                    return null;
                }

                _logger.Debug("Get Stock Data " + objResponse.stock + ": " + content);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get Stock Data Content:" + content + "|| Exception: " + e);
                Console.WriteLine("Get Stock Data Content:" + content + "|| Exception: " + e);
                return null;
            }
        }

        /// <summary>
        /// Inserts the stock data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task InsertStockData(StockData data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlStockYahoo);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                _logger.Debug("Insert Stock Data " + data.stock + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Stock Data " + data.stock + " Exception: " + e);
                Console.WriteLine("Insert Stock " + data.stock + " Data Exception: " + e);
            }
        }

        /// <summary>
        /// Deletes the stock data.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static async Task<string> DeleteStockData(string id)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlStockYahoo + "/" + id);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Delete Stock Data " + ": " + "OK");
                return "OK";
            }
            catch (Exception e)
            {
                _logger.Fatal("Delete Stock Data " + " Exception: " + e);
                Console.WriteLine("Delete Stock Data " + " Exception: " + e);
                return null;
            }
        }

        #endregion

        #region RealData

        /// <summary>
        /// Inserts the real data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task InsertRealData(StockData data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlRealData);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                Stock sdata = JsonConvert.DeserializeObject<Stock>(data.jsondata);

                RealData rdata = new RealData();
                rdata.guid = Guid.NewGuid().ToString();
                rdata._id = ObjectId.GenerateNewId();
                rdata.open = sdata.Open;
                rdata.stock = sdata.Symbol;
                rdata.ask = sdata.Ask;
                rdata.bid = sdata.Bid;
                rdata.change = sdata.Change;
                rdata.close = sdata.Previousclose;
                rdata.datetime = data.datetime;

                string jdata = JsonConvert.SerializeObject(rdata);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Insert Real Data " + data.stock + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Real " + data.stock + " Exception: " + e);
                Console.WriteLine("Insert Real " + data.stock + " Exception: " + e);
            }
        }
        #endregion

        #region AnnualData

        /// <summary>
        /// Inserts the annual data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task InsertAnnualData(AnnualData data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlAnnual);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Insert Annual Data " + data.stock + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Annual Data" + data.stock + " Exception: " + e);
                Console.WriteLine("Insert Annual Data" + data.stock + " Exception: " + e);
            }
        }

        /// <summary>
        /// Gets the annual databy stock.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns></returns>
        public static async Task<List<AnnualData>> GetAnnualDatabyStock(string stock)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlAnnual + "/" + stock);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                IEnumerable<AnnualData> objResponse_aux = JsonConvert.DeserializeObject<IEnumerable<AnnualData>>(content);
                List<AnnualData> objResponse = objResponse_aux.ToList();

                if (objResponse == null)
                {
                    return null;
                }

                _logger.Debug("Get Annual Data " + stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get Annual Data " + " Exception: " + e);
                Console.WriteLine("Get Annual Data " + " Exception: " + e);
                return null;
            }
        }
        #endregion

        #region FutureData

        /// <summary>
        /// Inserts the annual data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task InsertFutureData(FutureData data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlFuture);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Insert Future Data " + data.stock + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Future Data" + data.stock + " Exception: " + e);
                Console.WriteLine("Insert Future Data" + data.stock + " Exception: " + e);
            }
        }

        /// <summary>
        /// Gets the annual databy stock.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns></returns>
        public static async Task<List<FutureData>> GetFutureData(string stock)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlFuture + "/" + stock);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                IEnumerable<FutureData> objResponse_aux = JsonConvert.DeserializeObject<IEnumerable<FutureData>>(content);
                List<FutureData> objResponse = objResponse_aux.ToList();

                if (objResponse == null)
                {
                    return null;
                }

                _logger.Debug("Get FutureData " + stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get FutureData " + " Exception: " + e);
                Console.WriteLine("Get FutureData " + " Exception: " + e);
                return null;
            }
        }
        #endregion

        #region PastData

        public static async Task InsertPastData(PastData data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlPast);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Insert Past Data " + data.stock + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Past Data" + data.stock + " Exception: " + e);
                Console.WriteLine("Insert Past Data" + data.stock + " Exception: " + e);
            }
        }


        public static async Task<List<PastData>> GetPastData(string stock)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlPast + "/" + stock);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                IEnumerable<PastData> objResponse_aux = JsonConvert.DeserializeObject<IEnumerable<PastData>>(content);
                List<PastData> objResponse = objResponse_aux.ToList();

                if (objResponse == null)
                {
                    return null;
                }

                _logger.Debug("Get PastData " + stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get PastData " + " Exception: " + e);
                Console.WriteLine("Get PastData " + " Exception: " + e);
                return null;
            }
        }

        public static async Task<PastData> GetPastDataDes(string stock)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlPast + "/" + stock);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                PastData objResponse = JsonConvert.DeserializeObject<PastData>(content);

                if (objResponse == null)
                {
                    return new PastData() {date = DateTime.MinValue};
                }

                _logger.Debug("Get PastData " + stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get PastData " + " Exception: " + e);
                Console.WriteLine("Get PastData " + " Exception: " + e);
                return null;
            }
        }

        #endregion

        #region LastData

        public static async Task InsertLastData(FinanceWeb.Models.Lastdata data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlLast);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Insert Last Data " + data.symbol + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Last Data" + data.symbol + " Exception: " + e);
                Console.WriteLine("Insert Last Data" + data.symbol + " Exception: " + e);
            }
        }


        public static async Task<List<FinanceWeb.Models.Lastdata>> GetLastData(string stock)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlLast + "/" + stock);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                IEnumerable<FinanceWeb.Models.Lastdata> objResponse_aux = JsonConvert.DeserializeObject<IEnumerable<FinanceWeb.Models.Lastdata>>(content);
                List<FinanceWeb.Models.Lastdata> objResponse = objResponse_aux.ToList();

                if (objResponse == null)
                {
                    return null;
                }

                _logger.Debug("Get LastData " + stock);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get LastData " + " Exception: " + e);
                Console.WriteLine("Get LastData " + " Exception: " + e);
                return null;
            }
        }
        #endregion

        #region Execution

        public static async Task InsertExecution(Execution data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlExecution);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Insert Execution " + data.process + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Execution" + data.process + " Exception: " + e);
                Console.WriteLine("Insert Execution" + data.process + " Exception: " + e);
            }
        }


        public static async Task<Execution> GetExecution(string process)
        {
            string content = "";
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlExecution + "/" + process);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                content = reader.ReadToEnd();

                Execution objResponse = JsonConvert.DeserializeObject<Execution>(content);

                if (objResponse == null)
                {
                    Execution newExecution = new Execution();
                    newExecution.process = process;
                    newExecution._id = ObjectId.GenerateNewId();
                    newExecution.guid = Guid.NewGuid().ToString();
                    newExecution.isActive = true;
                    newExecution.isComplete = false;
                    newExecution.datetime = DateTime.MinValue;
                    newExecution.isRunning = false;

                    InsertExecution(newExecution).Wait();
                    return newExecution;
                }

                _logger.Debug("Get Execution " + process);
                return objResponse;
            }
            catch (Exception e)
            {
                _logger.Fatal("Get Execution Content:" + content +  "|| Exception: " + e);
                Console.WriteLine("Get Execution Content:" + content + "|| Exception: " + e);
                return null;
            }
        }

        public static async Task UpdateExecution(Execution data)
        {
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlExecution + "/" + data.process);
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.Credentials = new NetworkCredential("aaaaaaa", "xxx");

                string jdata = JsonConvert.SerializeObject(data);

                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(enc.GetBytes(jdata), 0, jdata.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();

                _logger.Debug("Update Execution " + data.process + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Update Execution" + data.process + " Exception: " + e);
                Console.WriteLine("Update Execution" + data.process + " Exception: " + e);
            }
        }
        #endregion

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        public static async Task Login()
        {
            LoginViewModel model = new LoginViewModel();
            try
            {
                // encoder
                UTF8Encoding enc = new UTF8Encoding();

                WebRequest request = WebRequest.Create(UrlLogin);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                model.Email = "aaaaaaa@gg.com";
                model.Password = "xxx";
                model.RememberMe = true;

                string authInfo = model.Email + ":" + model.Password;
                //authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                request.Headers["Authorization"] = "Basic " + authInfo;

                string jdata = "Email==false";

                byte[] byteArray = Encoding.UTF8.GetBytes(jdata);


                Stream dataStream = await request.GetRequestStreamAsync();
                dataStream.Write(byteArray, 0, byteArray.Length);

                WebResponse wr = await request.GetResponseAsync();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                _logger.Debug("Insert Seasonality Data " + ": " + content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Insert Seasonality " + " Exception: " + e);
                Console.WriteLine("Insert Seasonality " + " Exception: " + e);
            }
        }
    }
}
