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
    public class RealDataRepository : IRealDataRepository
    {
        private readonly DataContext _context = null;

        public RealDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<RealData>> GetAllData()
        {
            try
            {
                return await _context.RealData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<RealData> GetData(string id)
        {
            var filter = Builders<RealData>.Filter.Eq("Guid", id);

            try
            {
                return await _context.RealData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<RealData>> GetDatabyStock(string stock)
        {
            var filter = Builders<RealData>.Filter.Eq("Stock", stock);

            try
            {
                return await _context.RealData.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddData(RealData item)
        {
            try
            {
                await _context.RealData.InsertOneAsync(item);
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
                return await _context.RealData.DeleteOneAsync(
                    Builders<RealData>.Filter.Eq("Guid", id));
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
                return await _context.RealData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
