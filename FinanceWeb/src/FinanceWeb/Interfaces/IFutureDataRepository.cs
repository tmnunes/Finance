using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IFutureDataRepository
    {
        Task<IEnumerable<FutureData>> GetAllData();
        Task<FutureData> GetData(string id);
        Task<IEnumerable<FutureData>> GetDatabyStock(string stock);
        Task AddData(FutureData item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
