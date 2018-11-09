using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IStockLastDataRepository
    {
        Task<IEnumerable<Lastdata>> GetAllData();
        Task<Lastdata> GetData(string id);
        Task<Lastdata> GetDatabyStock(string stock);
        Task<Lastdata> GetDatabyStockDate(string stock, string date);
        Task<Lastdata> GetFirst();
        Task AddData(Lastdata item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
