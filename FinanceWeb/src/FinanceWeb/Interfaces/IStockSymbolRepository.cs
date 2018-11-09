using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FinanceWeb.Interfaces
{
    public interface IStockSymbolRepository
    {
        Task<IEnumerable<StockSymbol>> GetAllStockSymbols();
        Task<StockSymbol> GetStockSymbol(string id);
        Task<List<StockSymbol>> GetStockSymbolbySymbol(string name);
        Task AddStockSymbol(StockSymbol item);
        Task<DeleteResult> RemoveStockSymbol(string id);
        Task<UpdateResult> UpdateStockSymbol(string id, string body);
        Task<ReplaceOneResult> UpdateStockSymbolDocument(string id, string body);
        Task<DeleteResult> RemoveAllStockSymbol();
    }
}
