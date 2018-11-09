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
    public class StockScoreDataRepository : IStockScoreDataRepository
    {
        private readonly DataContext _context = null;

        public StockScoreDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<StockScoreData>> GetAllData()
        {
            try
            {
                return await _context.StockScoreData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockScoreData> GetData(string id)
        {
            var filter = Builders<StockScoreData>.Filter.Eq("Guid", id);

            try
            {
                return await _context.StockScoreData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockScoreData> GetDatabyStock(string stock)
        {
            var filter = Builders<StockScoreData>.Filter.Eq("Stock", stock);

            try
            {
                return await _context.StockScoreData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockScoreData> GetDatabyStockDate(string stock, string date)
        {
            DateTime dat = Convert.ToDateTime(date);
            //filterBuilder.Gte(x => x.CreatedOn, start) & filterBuilder.Lt(x => x.CreatedOn, end)
            var filter = (Builders<StockScoreData>.Filter.Eq("Stock", stock)) & 
                (Builders<StockScoreData>.Filter.Gte("Date", dat) & 
                (Builders<StockScoreData>.Filter.Lt("Date", dat.AddDays(1))));

            try
            {
                return await _context.StockScoreData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockScoreData> GetFirst()
        {
            try
            {
                return await _context.StockScoreData.Find(_ => true).Limit(1).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddData(StockScoreData item)
        {
            try
            {
                await _context.StockScoreData.InsertOneAsync(item);
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
                return await _context.StockScoreData.DeleteOneAsync(
                    Builders<StockScoreData>.Filter.Eq("Guid", id));
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
                return await _context.StockScoreData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
