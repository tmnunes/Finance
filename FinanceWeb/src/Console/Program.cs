using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NLog;
using FinanceWeb.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using RestSharp.Extensions;

namespace ConsoleGetter
{
    public class Program
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        #region Threads

        private static Thread _threadData;

        /// <summary>
        /// The thread process
        /// </summary>
        private static Thread _threadProcess;
        /// <summary>
        /// The thread
        /// </summary>
        private static Thread _thread;
        /// <summary>
        /// The thread a
        /// </summary>
        private static Thread _threadA;
        /// <summary>
        /// The thread b
        /// </summary>
        private static Thread _threadB;
        /// <summary>
        /// The thread c
        /// </summary>
        private static Thread _threadC;
        /// <summary>
        /// The thread d
        /// </summary>
        private static Thread _threadD;
        /// <summary>
        /// The thread f
        /// </summary>
        private static Thread _threadF;
        /// <summary>
        /// The thread e
        /// </summary>
        private static Thread _threadE;
        /// <summary>
        /// The thread g
        /// </summary>
        private static Thread _threadG;
        /// <summary>
        /// The thread h
        /// </summary>
        private static Thread _threadH;
        /// <summary>
        /// The thread i
        /// </summary>
        private static Thread _threadI;
        /// <summary>
        /// The thread j
        /// </summary>
        private static Thread _threadJ;
        /// <summary>
        /// The thread k
        /// </summary>
        private static Thread _threadK;
        /// <summary>
        /// The thread l
        /// </summary>
        private static Thread _threadL;
        /// <summary>
        /// The thread m
        /// </summary>
        private static Thread _threadM;
        /// <summary>
        /// The thread n
        /// </summary>
        private static Thread _threadN;
        /// <summary>
        /// The thread o
        /// </summary>
        private static Thread _threadO;
        /// <summary>
        /// The thread p
        /// </summary>
        private static Thread _threadP;
        /// <summary>
        /// The thread q
        /// </summary>
        private static Thread _threadQ;
        /// <summary>
        /// The thread r
        /// </summary>
        private static Thread _threadR;
        /// <summary>
        /// The thread s
        /// </summary>
        private static Thread _threadS;
        /// <summary>
        /// The thread t
        /// </summary>
        private static Thread _threadT;
        /// <summary>
        /// The thread u
        /// </summary>
        private static Thread _threadU;
        /// <summary>
        /// The thread v
        /// </summary>
        private static Thread _threadV;
        /// <summary>
        /// The thread w
        /// </summary>
        private static Thread _threadW;
        /// <summary>
        /// The thread x
        /// </summary>
        private static Thread _threadX;
        /// <summary>
        /// The thread y
        /// </summary>
        private static Thread _threadY;
        /// <summary>
        /// The thread z
        /// </summary>
        private static Thread _threadZ;
        #endregion

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            //logger.Trace("Sample trace message");
            //logger.Debug("Sample debug message");
            //logger.Info("Sample informational message");
            //logger.Warn("Sample warning message");
            //logger.Error("Sample error message");
            //logger.Fatal("Sample fatal error message");

            Console.ReadLine();

            ServiceCom.LoadStocks().Wait();

            //Login().Wait();

            #region Threads
            _thread = new Thread(ThLoad);
            _threadProcess = new Thread(ThProcessRealData);
            _threadData = new Thread(ThProcessData);
            _thread.Start();
            _threadProcess.Start();
            _threadData.Start();
            _thread.Join();
            _threadProcess.Join();
            _threadData.Join();
            #endregion
        }

        /// <summary>
        /// Thes the load.
        /// </summary>
        static void ThLoad()
        {
            while (true)
            {
                var ThMain = ServiceCom.GetExecution("Main");
                ThMain.Wait();
                var main = ThMain.Result;


                if (main.isRunning)
                {
                    //continue
                }
                else
                {
                    main.isRunning = true;
                    var ThM = ServiceCom.UpdateExecution(main);
                    ThM.Wait();
                    ServiceCom._stocks_aux = ServiceCom._stocks;

                    string let = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                    ProcSeasonality = new List<Execution>();
                    ProcScore = new List<Execution>();
                    ProcReal = new List<Execution>();

                    foreach (char l in let)
                    {
                        var ThSeasonality = ServiceCom.GetExecution("ThSeasonality" + l);
                        ThSeasonality.Wait();
                        ProcSeasonality.Add(ThSeasonality.Result);
                        var ThScore = ServiceCom.GetExecution("ThScore" + l);
                        ThScore.Wait();
                        ProcScore.Add(ThScore.Result);
                        var ThReal = ServiceCom.GetExecution("ThReal" + l);
                        ThReal.Wait();
                        ProcReal.Add(ThReal.Result);
                    }

                    #region Threads

                    _threadA = new Thread(() => Thloop("A"));
                    _threadB = new Thread(() => Thloop("B"));
                    _threadC = new Thread(() => Thloop("C"));
                    _threadD = new Thread(() => Thloop("D"));
                    _threadF = new Thread(() => Thloop("F"));
                    _threadE = new Thread(() => Thloop("E"));
                    _threadG = new Thread(() => Thloop("G"));
                    _threadH = new Thread(() => Thloop("H"));
                    _threadI = new Thread(() => Thloop("I"));
                    _threadJ = new Thread(() => Thloop("J"));
                    _threadK = new Thread(() => Thloop("K"));
                    _threadL = new Thread(() => Thloop("L"));
                    _threadM = new Thread(() => Thloop("M"));
                    _threadN = new Thread(() => Thloop("N"));
                    _threadO = new Thread(() => Thloop("O"));
                    _threadP = new Thread(() => Thloop("P"));
                    _threadQ = new Thread(() => Thloop("Q"));
                    _threadR = new Thread(() => Thloop("R"));
                    _threadS = new Thread(() => Thloop("S"));
                    _threadT = new Thread(() => Thloop("T"));
                    _threadU = new Thread(() => Thloop("U"));
                    _threadV = new Thread(() => Thloop("V"));
                    _threadW = new Thread(() => Thloop("W"));
                    _threadX = new Thread(() => Thloop("X"));
                    _threadY = new Thread(() => Thloop("Y"));
                    _threadZ = new Thread(() => Thloop("Z"));
                    _threadA.Start();
                    _threadB.Start();
                    _threadC.Start();
                    _threadD.Start();
                    _threadF.Start();
                    _threadE.Start();
                    _threadG.Start();
                    _threadH.Start();
                    _threadI.Start();
                    _threadJ.Start();
                    _threadK.Start();
                    _threadL.Start();
                    _threadM.Start();
                    _threadN.Start();
                    _threadP.Start();
                    _threadQ.Start();
                    _threadR.Start();
                    _threadS.Start();
                    _threadT.Start();
                    _threadU.Start();
                    _threadV.Start();
                    _threadW.Start();
                    _threadX.Start();
                    _threadY.Start();
                    _threadZ.Start();
                    _threadO.Start();
                    _threadA.Join();
                    _threadB.Join();
                    _threadC.Join();
                    _threadD.Join();
                    _threadF.Join();
                    _threadE.Join();
                    _threadG.Join();
                    _threadH.Join();
                    _threadI.Join();
                    _threadJ.Join();
                    _threadK.Join();
                    _threadL.Join();
                    _threadM.Join();
                    _threadN.Join();
                    _threadP.Join();
                    _threadQ.Join();
                    _threadR.Join();
                    _threadS.Join();
                    _threadT.Join();
                    _threadU.Join();
                    _threadV.Join();
                    _threadW.Join();
                    _threadX.Join();
                    _threadY.Join();
                    _threadZ.Join();
                    _threadO.Join();

                    #endregion
                }

                _logger.Info("Financhill get sleep ");
                Console.WriteLine("Financhill get sleep ");

                Thread.Sleep(3600000); // one hour
            }
        }

        private static List<Execution> ProcSeasonality;
        private static List<Execution> ProcScore ;
        private static List<Execution> ProcReal;

        /// <summary>
        /// Thloops the specified letter.
        /// </summary>
        /// <param name="letter">The letter.</param>
        static void Thloop(string letter)
        {
            Execution ProcSeasonality_aux = ProcSeasonality.Find(p => p.process == "ThSeasonality" + letter);
            Execution ProcScore_aux = ProcScore.Find(p => p.process == "ThScore" + letter);
            Execution ProcReal_aux = ProcReal.Find(p => p.process == "ThReal" + letter);

            int index=0;
            while (true)
            {
                try
                {
                    var st = ServiceCom._stocks_aux.FirstOrDefault(p => p.symbol.StartsWith(letter));

                    if (st != null)
                    {
                        if (ProcSeasonality_aux.datetime.Date != DateTime.Now.Date)
                        {
                            var proc = OutsideCom.RequestFinanchill(st);
                            proc.Wait();
                            if (proc.Result != null)
                                ServiceCom.InsertSeasonality(proc.Result).Wait();
                        }

                        if (ProcScore_aux.datetime.Date != DateTime.Now.Date)
                        {
                            var proc2 = OutsideCom.RequestFinanchillScore(st);
                            proc2.Wait();
                            if (proc2.Result != null)
                                ServiceCom.InsertScoreData(proc2.Result).Wait();
                        }

                        if (ProcReal_aux.datetime.Date != DateTime.Now.Date)
                        {
                            var proc3 = OutsideCom.GetDataGFinance(st);
                            proc3.Wait();
                            if (proc3.Result != null)
                                ServiceCom.InsertStockData(proc3.Result).Wait();

                        }
                        index++;
                        ServiceCom._stocks_aux.Remove(st);
                    }
                    else
                    {
                        break;
                    }

                    Thread.Sleep(200);
                }
                catch (Exception e)
                {
                    _logger.Fatal("Thread " + letter + " Exception: " + e);
                    Console.WriteLine("Thread " + letter + " Exception: " + e);
                }
            }

            if (index >= 1)
            {
                ProcSeasonality_aux.isComplete = true;
                ProcSeasonality_aux.isActive = false;
                ProcSeasonality_aux.datetime = DateTime.Now;
                ProcScore_aux.isComplete = true;
                ProcScore_aux.isActive = false;
                ProcScore_aux.datetime = DateTime.Now;
                ProcReal_aux.isComplete = true;
                ProcReal_aux.isActive = false;
                ProcReal_aux.datetime = DateTime.Now;

                var ThSea = ServiceCom.UpdateExecution(ProcSeasonality_aux);
                ThSea.Wait();
                var ThSco = ServiceCom.UpdateExecution(ProcScore_aux);
                ThSco.Wait();
                var ThRea = ServiceCom.UpdateExecution(ProcReal_aux);
                ThRea.Wait();
            }
        }

        /// <summary>
        /// Thes the process real data.
        /// </summary>
        static void ThProcessRealData()
        {
            while (true)
            {
                ProcessRealData().Wait();
                Thread.Sleep(500);
            }
        }



        /// <summary>
        /// Processes the real data.
        /// </summary>
        /// <returns></returns>
        public static async Task ProcessRealData()
        {
            try
            {
                StockData objResponse = ServiceCom.GetFirstStockData().Result;

                if (objResponse == null || objResponse.guid == null || objResponse.guid == "")
                {
                    Console.WriteLine("ProcessRealData Sleep");
                    Thread.Sleep(60000); // 
                }
                else
                {
                    ServiceCom.InsertRealData(objResponse).Wait();

                    ServiceCom.DeleteStockData(objResponse.guid).Wait();
                }

            }
            catch (Exception e)
            {
                _logger.Fatal("Request ProcessRealData Exception: " + e);
                Console.WriteLine("Request ProcessRealData Exception: " + e);
            }
        }


        /// <summary>
        /// Thes the process data.
        /// </summary>
        static void ThProcessData()
        {
            while (true)
            {
                ProcessData().Wait();
                Thread.Sleep(500);
            }
        }



        /// <summary>
        /// Processes the data.
        /// </summary>
        /// <returns></returns>
        public static async Task ProcessData()
        {
            try
            {
                StockSeasonalityData objResponseSeasonality = ServiceCom.GetFirstSeasonality().Result;
                DataSeasonality objResponseaux = null;

                if (objResponseSeasonality != null)
                {
                    objResponseaux =
                        JsonConvert.DeserializeObject<DataSeasonality>(objResponseSeasonality.jsondata);
                }

                if (objResponseaux == null || objResponseaux.symbol == null || objResponseaux.symbol == "")
                {
                    Console.WriteLine("ProcessData Sleep");
                    Thread.Sleep(60000); // 
                }
                else
                {
                    InjectorAnnualData(objResponseaux).Wait();
                    InjectorFutureData(objResponseSeasonality, objResponseaux).Wait();
                    InjectorPastData(objResponseSeasonality, objResponseaux).Wait();
                }

            }
            catch (Exception e)
            {
                _logger.Fatal("Request ProcessData Exception: " + e);
                Console.WriteLine("Request ProcessData Exception: " + e);
            }
        }


        /// <summary>
        /// Injectors the annual data.
        /// </summary>
        /// <param name="objResponseSeasonality">The object response seasonality.</param>
        /// <returns></returns>
        public static async Task InjectorAnnualData(DataSeasonality objResponseSeasonality)
        {
            try
            {
                var annual = ServiceCom.GetAnnualDatabyStock(objResponseSeasonality.symbol);
                annual.Wait();
                string yy = DateTime.Now.AddYears(-1).ToString("yy");
                var findannual = annual.Result.Find(p => p.year == yy);

                if (findannual != null && findannual.year.Any())
                {
                    //continue
                }
                else
                {
                    foreach (var first in objResponseSeasonality.annualDataArray)
                    {
                        var find = annual.Result.Find(p => p.year == first[0].ToString());
                        if (find != null && find.year.Any())
                        {
                            //continue
                        }
                        else
                        {
                            AnnualData data = new AnnualData();
                            data._id = ObjectId.GenerateNewId();
                            data.guid = Guid.NewGuid().ToString();
                            data.year = first[0].ToString();
                            data.value = first[1].ToString();
                            data.stock = objResponseSeasonality.symbol;
                            data.timestamp = DateTime.Now;

                            ServiceCom.InsertAnnualData(data).Wait();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Fatal("Request Injector Annual Data Exception: " + e);
                Console.WriteLine("Request Injector Annual Data Exception: " + e);
            }
        }

        public static async Task InjectorFutureData(StockSeasonalityData objSeasonalData, DataSeasonality objResponseSeasonality)
        {
            try
            {
                foreach (var arr in objResponseSeasonality.chartArray)
                {
                    if (arr.Count == 5 && arr[1] == null && arr[2] == null && arr[3] != null && arr[4] != null)
                    {
                        string obj0 = arr[0].ToString();
                        obj0 = obj0.Substring(5);
                        obj0 = obj0.Substring(0,obj0.Length-1);
                        var obj0array = obj0.Split(',');

                        DateTime date = DateTime.Parse(obj0array[0] + "/" + (Convert.ToInt32(obj0array[1])+1).ToString() + "/" + obj0array[2]);

                        string hprob = arr[4].ToString();

                        hprob = hprob.Substring(0, hprob.Length -2);
                        hprob = hprob.Substring(hprob.Length -3);
                        hprob = hprob.Replace(" ", string.Empty);

                        FutureData fdata = new FutureData();
                        fdata.datetime = date;
                        fdata.timestamp = DateTime.Now;
                        fdata.guid = Guid.NewGuid().ToString();
                        fdata._id = ObjectId.GenerateNewId();
                        fdata.stock = objSeasonalData.stock;
                        fdata.histprobability = hprob;
                        fdata.value = arr[3].ToString();

                        ServiceCom.InsertFutureData(fdata).Wait();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Fatal("Request Injector Future Data  Exception: " + e);
                Console.WriteLine("Request Injector Future Data Exception: " + e);
            }
        }

        public static async Task InjectorPastData(StockSeasonalityData objSeasonalData, DataSeasonality objResponseSeasonality)
        {
            try
            {
                string dat = objSeasonalData.datetime.ToString("yyyy-MM-dd");
                StockScoreData score = ServiceCom.GetScorebyDate(objSeasonalData.stock, dat).Result;

                if (score == null)
                {
                    ServiceCom.DeleteSeasonalityData(objSeasonalData.guid);
                }
                else
                {
                    List<object[]> chart = new List<object[]>();
                    List<Score> scores = new List<Score>();
                    List<int> median = new List<int>();
                    List<object[]> indicatorADL = new List<object[]>();
                    List<object[]> indicatorMACD = new List<object[]>();
                    List<object[]> indicatorRSI14 = new List<object[]>();
                    Lastdata last = new Lastdata();

                    if (score != null)
                    {
                        DataScore objResponseScoreaux = JsonConvert.DeserializeObject<DataScore>(score.jsondata);
                        chart = objResponseScoreaux.symbols.stocks.chart.ToList();
                        scores = objResponseScoreaux.symbols.stocks.scores.ToList();
                        median = objResponseScoreaux.symbols.stocks.scoreMedian.ToList();
                        indicatorADL = objResponseScoreaux.symbols.stocks.indicators.ADL.ToList();
                        indicatorMACD = objResponseScoreaux.symbols.stocks.indicators.MACD.ToList();
                        indicatorRSI14 = objResponseScoreaux.symbols.stocks.indicators.RSI14.ToList();
                        last = objResponseScoreaux.symbols.stocks.lastData;
                    }

                    PastData oldpdata = ServiceCom.GetPastDataDes(objSeasonalData.stock).Result;

                    foreach (var arr in objResponseSeasonality.chartArray)
                    {
                        if (arr.Count == 5 && arr[1] != null && arr[2] != null)
                        {
                            string obj0 = arr[0].ToString();
                            obj0 = obj0.Substring(5);
                            obj0 = obj0.Substring(0, obj0.Length - 1);
                            var obj0array = obj0.Split(',');

                            DateTime date = DateTime.Parse(obj0array[0] + "/" +
                                                           (Convert.ToInt32(obj0array[1]) + 1).ToString() + "/" +
                                                           obj0array[2]);

                            if (date <= oldpdata.date)
                            {
                                // continue
                            }
                            else
                            {
                                PastData pdata = new PastData();
                                pdata.date = date;

                                int index = 0;

                                #region Chart

                                foreach (var cha in chart)
                                {
                                    if (arr[0].ToString() == cha[0].ToString())
                                    {
                                        List<string> lmessage = new List<string>();
                                        if (cha.Length >= 7)
                                            lmessage.Add(cha[6].ToString());
                                        if (cha.Length >= 9)
                                            lmessage.Add(cha[8].ToString());
                                        if (cha.Length >= 11)
                                            lmessage.Add(cha[10].ToString());
                                        if (cha.Length >= 13)
                                            lmessage.Add(cha[12].ToString());
                                        if (cha.Length >= 15)
                                            lmessage.Add(cha[14].ToString());
                                        if (cha.Length >= 17)
                                            lmessage.Add(cha[16].ToString());
                                        if (cha.Length >= 19)
                                            lmessage.Add(cha[18].ToString());
                                        if (cha.Length >= 21)
                                            lmessage.Add(cha[20].ToString());
                                        if (cha.Length >= 23)
                                            lmessage.Add(cha[22].ToString());
                                        if (cha.Length >= 25)
                                            lmessage.Add(cha[24].ToString());
                                        if (cha.Length >= 27)
                                            lmessage.Add(cha[26].ToString());
                                        if (cha.Length >= 29)
                                            lmessage.Add(cha[28].ToString());
                                        if (cha.Length >= 31)
                                            lmessage.Add(cha[30].ToString());
                                        if (cha.Length >= 33)
                                            lmessage.Add(cha[32].ToString());

                                        pdata.chartmessage = lmessage;

                                        index = chart.IndexOf(cha);
                                        break;
                                    }
                                }

                                #endregion

                                #region Median

                                if (median.Count >= 1)
                                    pdata.scoreMedian = median[0].ToString();

                                #endregion

                                #region Scores

                                foreach (var sco in scores)
                                {
                                    if (date.ToString("yyyy-MM-dd") == sco.date)
                                    {
                                        pdata.score = sco.score;
                                        pdata.signal = sco.signal;
                                        break;
                                    }
                                }

                                #endregion

                                #region Indicators

                                foreach (var IADL in indicatorADL)
                                {
                                    if (arr[0].ToString() == IADL[0].ToString())
                                    {
                                        pdata.adl = IADL[1].ToString();
                                        break;
                                    }
                                }

                                foreach (var MACD in indicatorMACD)
                                {
                                    if (arr[0].ToString() == MACD[0].ToString())
                                    {
                                        pdata.macd = MACD[1].ToString();
                                        break;
                                    }
                                }

                                foreach (var RSI14 in indicatorRSI14)
                                {
                                    if (arr[0].ToString() == RSI14[0].ToString())
                                    {
                                        pdata.rsi14 = RSI14[1].ToString();
                                        break;
                                    }
                                }

                                #endregion

                                pdata.value = arr[1].ToString();
                                pdata.timestamp = DateTime.Now;
                                pdata.guid = Guid.NewGuid().ToString();
                                pdata._id = ObjectId.GenerateNewId();
                                pdata.stock = objSeasonalData.stock;

                                ServiceCom.InsertPastData(pdata).Wait();
                            }
                        }
                    }

                    #region Lastdata

                    FinanceWeb.Models.Lastdata ldata = new FinanceWeb.Models.Lastdata();
                    ldata.symbol = last.symbol;
                    ldata.date = last.date;
                    ldata.week = last.week;
                    ldata.month = last.month;
                    ldata.year = last.year;
                    ldata.close = last.close;
                    ldata.low = last.low;
                    ldata.open = last.open;
                    ldata.high = last.high;
                    ldata.SMA8 = last.SMA8;
                    ldata.SMA21 = last.SMA21;
                    ldata.SMA25 = last.SMA25;
                    ldata.SMA50 = last.SMA50;
                    ldata.SMA200 = last.SMA200;
                    ldata.SMA100 = last.SMA100;
                    ldata.EMA8 = last.EMA8;
                    ldata.EMA20 = last.EMA20;
                    ldata.EMA50 = last.EMA50;
                    ldata.EMA200 = last.EMA200;
                    ldata.RSI14 = last.RSI14;
                    ldata.MACD = last.MACD;
                    ldata.ADLVolume = last.ADLVolume;
                    ldata.STDDEV25 = last.STDDEV25;
                    ldata.STDDEV100 = last.STDDEV100;
                    ldata._id = ObjectId.GenerateNewId();
                    ldata.guid = Guid.NewGuid().ToString();

                    ServiceCom.InsertLastData(ldata).Wait();

                    #endregion

                    ServiceCom.DeleteScoreData(score.guid);
                    ServiceCom.DeleteSeasonalityData(objSeasonalData.guid);
                }
            }
            catch (Exception e)
            {
                _logger.Fatal("Request Injector Future Data  Exception: " + e);
                Console.WriteLine("Request Injector Future Data Exception: " + e);
            }
        }

    }
}
