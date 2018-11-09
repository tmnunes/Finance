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
    public class StockSeasonalityDataRepository : IStockSeasonalityDataRepository
    {
        private readonly DataContext _context = null;

        public StockSeasonalityDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<StockSeasonalityData>> GetAllData()
        {
            try
            {
                return await _context.StockSeasonalityData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockSeasonalityData> GetData(string id)
        {
            var filter = Builders<StockSeasonalityData>.Filter.Eq("Guid", id);

            try
            {
                return await _context.StockSeasonalityData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockSeasonalityData> GetFirst()
        {
            try
            {
                return await _context.StockSeasonalityData.Find(_ => true).Limit(1).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddData(StockSeasonalityData item)
        {
            try
            {
                await _context.StockSeasonalityData.InsertOneAsync(item);
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
                return await _context.StockSeasonalityData.DeleteOneAsync(
                    Builders<StockSeasonalityData>.Filter.Eq("Guid", id));
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
                return await _context.StockSeasonalityData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
