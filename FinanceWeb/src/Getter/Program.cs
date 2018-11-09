using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog;
using FinanceWeb.Models;

namespace Getter
{
    public class Program
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The copy flag
        /// </summary>
        private static bool copyFlag = true;

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
            //_threadA = new Thread(() => Thloop("A"));
            //_threadA.Start();
            //_threadA.Join();

            while (true)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && copyFlag)
                {
                    copyFlag = false;
                    ServiceCom._stocks_aux = ServiceCom._stocks;
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

                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && copyFlag)
                {
                    copyFlag = true;
                }

                _logger.Info("Financhill sleep ");
                Console.WriteLine("Financhill sleep ");
                Thread.Sleep(86400000); // one day
            }
        }

        /// <summary>
        /// Thloops the specified letter.
        /// </summary>
        /// <param name="letter">The letter.</param>
        static void Thloop(string letter)
        {
            while (true)
            {
                try
                {
                    var st = ServiceCom._stocks_aux.FirstOrDefault(p => p.symbol.StartsWith(letter));

                    if (st != null)
                    {
                        var proc = OutsideCom.RequestFinanchill(st);
                        proc.Wait();
                        if (proc.Result != null)
                            ServiceCom.InsertSeasonality(proc.Result).Wait();

                        var proc2 = OutsideCom.RequestFinanchillScore(st);
                        proc2.Wait();
                        if (proc2.Result != null)
                            ServiceCom.InsertScoreData(proc2.Result).Wait();

                        var proc3 = OutsideCom.GetDataGFinance(st);
                        proc3.Wait();
                        if (proc3.Result != null)
                            ServiceCom.InsertStockData(proc3.Result).Wait();

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
                    //continue
                }
                else
                {
                    ServiceCom.InsertRealData(objResponse).Wait();

                    ServiceCom.DeleteStockData(objResponse.guid).Wait();
                }

            }
            catch (Exception e)
            {
                _logger.Fatal("Request Financhill Seasonality Exception: " + e);
                Console.WriteLine("Request Financhill Seasonality Exception: " + e);
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

				DataSeasonality objResponseaux = JsonConvert.DeserializeObject<DataSeasonality>(objResponseSeasonality.jsondata);

				if (objResponseaux == null || objResponseaux.symbol == null || objResponseaux.symbol == "")
                {
                    StockScoreData objResponseScore = ServiceCom.GetFirstScore().Result;
                }
                else
                {
                    InjectorAnnualData(objResponseaux);
                    InjectorFutureData(objResponseSeasonality,objResponseaux);
                    //DataScore objResponseScore = ServiceCom.GetScore(objResponseaux.symbol).Result;
                }

            }
            catch (Exception e)
            {
                _logger.Fatal("Request Financhill Seasonality Exception: " + e);
                Console.WriteLine("Request Financhill Seasonality Exception: " + e);
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
                        if (find != null && find.year.Any() )
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

		public static async Task InjectorFutureData(StockSeasonalityData objSeasonalData ,DataSeasonality objResponseSeasonality)
		{
			try
			{
                foreach (var arr in objResponseSeasonality.chartArray)
                {
                    if (arr.Count == 5 && arr[1] == null && arr[2] == null && arr[3] != null && arr[4] != null)
                    {
						//"Date(2017, 5, 2)"
						//"Week ending 06/02/2017\n\nNVEE projected to fall by -0.5230% and close at $36.56.  The historical probability is 66%."
						var www = arr[4].ToString().Split('.');

                        FutureData fdata = new FutureData();
                        fdata.datetime = Convert.ToDateTime(arr[0]);
                        fdata.timestamp = DateTime.Now;
                        fdata.guid = Guid.NewGuid().ToString();
                        fdata._id = ObjectId.GenerateNewId();
                        fdata.stock = objSeasonalData.stock;
                        //fdata.histprobability = ;
                        fdata.value = arr[3].ToString();
                    }
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
