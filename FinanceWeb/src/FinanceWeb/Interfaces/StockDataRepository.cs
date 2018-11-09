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
    public class StockDataRepository : IStockDataRepository
    {
        private readonly DataContext _context = null;

        public StockDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<StockData>> GetAllData()
        {
            try
            {
                return await _context.StockData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockData> GetData(string id)
        {
            var filter = Builders<StockData>.Filter.Eq("Guid" , id );

            try
            {
                return await _context.StockData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

		public async Task<StockData> GetFirst()
		{
			try
			{                                                
                return await _context.StockData.Find(_ => true).Limit(1).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				// log or manage the exception
				throw ex;
			}
		}

        public async Task AddData(StockData item)
        {
            try
            {
                await _context.StockData.InsertOneAsync(item);
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
                return await _context.StockData.DeleteOneAsync(
                    Builders<StockData>.Filter.Eq("Guid", id));
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
                return await _context.StockData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
