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
    public class FutureDataRepository : IFutureDataRepository
    {
        private readonly DataContext _context = null;

        public FutureDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<FutureData>> GetAllData()
        {
            try
            {
                return await _context.FutureData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<FutureData> GetData(string id)
        {
            var filter = Builders<FutureData>.Filter.Eq("Guid", id);

            try
            {
                return await _context.FutureData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<FutureData>> GetDatabyStock(string stock)
        {
            var filter = Builders<FutureData>.Filter.Eq("Stock", stock);

            try
            {
                return await _context.FutureData.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddData(FutureData item)
        {
            try
            {
                await _context.FutureData.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveData(string id)
        {
            try
            {
                return await _context.FutureData.DeleteOneAsync(
                    Builders<FutureData>.Filter.Eq("Guid", id));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveAll()
        {
            try
            {
                return await _context.FutureData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
