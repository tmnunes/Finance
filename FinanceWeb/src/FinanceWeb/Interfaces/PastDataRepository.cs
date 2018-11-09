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
    public class PastDataRepository : IPastDataRepository
    {
        private readonly DataContext _context = null;

        public PastDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<PastData>> GetAllData()
        {
            try
            {
                return await _context.PastData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<PastData> GetData(string id)
        {
            var filter = Builders<PastData>.Filter.Eq("Guid", id);

            try
            {
                return await _context.PastData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<PastData>> GetDatabyStock(string stock)
        {
            var filter = Builders<PastData>.Filter.Eq("Stock", stock);

            try
            {
                return await _context.PastData.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<PastData> GetPastbyStockDes(string stock)
        {
            var filter = Builders<PastData>.Filter.Eq("Stock", stock);

            var filter2 = (Builders<PastData>.Sort.Descending("Date"));

            try
            {
                return await _context.PastData
                    .Find(filter).Sort(filter2)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddData(PastData item)
        {
            try
            {
                await _context.PastData.InsertOneAsync(item);
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
                return await _context.PastData.DeleteOneAsync(
                    Builders<PastData>.Filter.Eq("Guid", id));
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
                return await _context.PastData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
