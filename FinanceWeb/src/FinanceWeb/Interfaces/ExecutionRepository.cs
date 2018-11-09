using FinanceWeb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace FinanceWeb.Interfaces
{
    public class ExecutionRepository : IExecutionRepository
    {
        private readonly DataContext _context = null;

        public ExecutionRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<Execution>> GetAllExecutions()
        {
            try
            {
                return await _context.Execution.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Execution> GetExecution(string id)
        {
            var filter = Builders<Execution>.Filter.Eq("Guid", id);

            try
            {
                return await _context.Execution
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Execution> GetExecutionbyprocess(string process)
        {
            var filter = Builders<Execution>.Filter.Eq("Process", process);

            try
            {
                return await _context.Execution
                    .Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddExecution(Execution item)
        {
            try
            {
                await _context.Execution.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveExecution(string id)
        {
            try
            {
                return await _context.Execution.DeleteOneAsync(
                    Builders<Execution>.Filter.Eq("Guid", id));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UpdateResult> UpdateExecution(string process, Execution body)
        {
            var filter = Builders<Execution>.Filter.Eq(s => s.process, process);
            var update = Builders<Execution>.Update
                .Set(s => s.datetime, body.datetime).Set(s => s.isComplete, body.isComplete).Set(s => s.isActive, body.isActive);
            //.CurrentDate(s => s.UpdatedOn);

            try
            {
                return await _context.Execution.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
