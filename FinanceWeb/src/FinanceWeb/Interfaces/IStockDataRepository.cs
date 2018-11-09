using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IStockDataRepository
    {
        Task<IEnumerable<StockData>> GetAllData();
        Task<StockData> GetData(string id);
        Task<StockData> GetFirst();
        Task AddData(StockData item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
