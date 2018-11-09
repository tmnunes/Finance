using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IRealDataRepository
    {
        Task<IEnumerable<RealData>> GetAllData();
        Task<RealData> GetData(string id);
        Task<IEnumerable<RealData>> GetDatabyStock(string stock);
        Task AddData(RealData item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
