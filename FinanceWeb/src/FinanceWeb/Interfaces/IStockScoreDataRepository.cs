using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IStockScoreDataRepository
    {
        Task<IEnumerable<StockScoreData>> GetAllData();
        Task<StockScoreData> GetData(string id);
        Task<StockScoreData> GetDatabyStock(string stock);
        Task<StockScoreData> GetDatabyStockDate(string stock, string date);
        Task<StockScoreData> GetFirst();
        Task AddData(StockScoreData item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
