using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IStockSeasonalityDataRepository
    {
        Task<IEnumerable<StockSeasonalityData>> GetAllData();
        Task<StockSeasonalityData> GetData(string id);
        Task<StockSeasonalityData> GetFirst();
        Task AddData(StockSeasonalityData item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
