using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using System;

namespace FinanceWeb.Interfaces
{
    public interface IAnnualDataRepository
    {
        Task<IEnumerable<AnnualData>> GetAllData();
        long Count();
        Task<AnnualData> GetData(string id);
        Task<IEnumerable<AnnualData>> GetDatabyStock(string stock);
        Task AddData(AnnualData item);
        Task<DeleteResult> RemoveData(string id);
        Task<DeleteResult> RemoveAll();
    }
}
