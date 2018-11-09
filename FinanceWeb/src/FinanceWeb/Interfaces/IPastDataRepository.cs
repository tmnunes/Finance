using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IPastDataRepository
    {
        Task<IEnumerable<PastData>> GetAllData();
        Task<PastData> GetData(string id);
        Task<IEnumerable<PastData>> GetDatabyStock(string stock);
        Task<PastData> GetPastbyStockDes(string stock);
        Task AddData(PastData item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
