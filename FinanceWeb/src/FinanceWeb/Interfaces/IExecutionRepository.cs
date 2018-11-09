using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceWeb.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FinanceWeb.Interfaces
{
    public interface IExecutionRepository
    {
        Task<IEnumerable<Execution>> GetAllExecutions();
        Task<Execution> GetExecution(string id);
        Task<Execution> GetExecutionbyprocess(string process);
        Task AddExecution(Execution item);
        Task<DeleteResult> RemoveExecution(string id);
        Task<UpdateResult> UpdateExecution(string process, Execution body);
    }
}
