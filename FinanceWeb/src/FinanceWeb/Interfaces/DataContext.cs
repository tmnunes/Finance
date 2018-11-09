using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceWeb.Controllers;
using FinanceWeb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinanceWeb.Interfaces
{
    public class DataContext
    {
        private readonly IMongoDatabase _database = null;

        public DataContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<StockSymbol> StockSymbols
        {
            get
            {
                return _database.GetCollection<StockSymbol>("StockSymbol");
            }
        }

        public IMongoCollection<StockSeasonalityData> StockSeasonalityData
        {
            get
            {
                return _database.GetCollection<StockSeasonalityData>("StockSeasonalityData");
            }
        }

        public IMongoCollection<StockScoreData> StockScoreData
        {
            get
            {
                return _database.GetCollection<StockScoreData>("StockScoreData");
            }
        }

        public IMongoCollection<StockData> StockData
        {
            get
            {
                return _database.GetCollection<StockData>("StockData");
            }
        }

        public IMongoCollection<RealData> RealData
        {
            get
            {
                return _database.GetCollection<RealData>("RealData");
            }
        }

        public IMongoCollection<PastData> PastData
        {
            get
            {
                return _database.GetCollection<PastData>("PastData");
            }
        }

        public IMongoCollection<FutureData> FutureData
        {
            get
            {
                return _database.GetCollection<FutureData>("FutureData");
            }
        }

        public IMongoCollection<AnnualData> AnnualData
        {
            get
            {
                return _database.GetCollection<AnnualData>("AnnualData");
            }
        }

        public IMongoCollection<Lastdata> LastData
        {
            get
            {
                return _database.GetCollection<Lastdata>("LastData");
            }
        }

        public IMongoCollection<Execution> Execution
        {
            get
            {
                return _database.GetCollection<Execution>("Execution");
            }
        }
    }
}
