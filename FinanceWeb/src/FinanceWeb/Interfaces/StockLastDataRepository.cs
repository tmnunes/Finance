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
    public class StockLastDataRepository : IStockLastDataRepository
    {
        private readonly DataContext _context = null;

        public StockLastDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<Lastdata>> GetAllData()
        {
            try
            {
                return await _context.LastData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Lastdata> GetData(string id)
        {
            var filter = Builders<Lastdata>.Filter.Eq("Guid", id);

            try
            {
                return await _context.LastData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Lastdata> GetDatabyStock(string stock)
        {
            var filter = Builders<Lastdata>.Filter.Eq("Stock", stock);

            try
            {
                return await _context.LastData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Lastdata> GetDatabyStockDate(string stock, string date)
        {
            DateTime dat = Convert.ToDateTime(date);
            //filterBuilder.Gte(x => x.CreatedOn, start) & filterBuilder.Lt(x => x.CreatedOn, end)
            var filter = (Builders<Lastdata>.Filter.Eq("Stock", stock)) & 
                (Builders<Lastdata>.Filter.Gte("Date", dat) & 
                (Builders<Lastdata>.Filter.Lt("Date", dat.AddDays(1))));

            try
            {
                var e = await _context.LastData
                    .Find(filter)
                    .FirstOrDefaultAsync();
                return e;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Lastdata> GetFirst()
        {
            try
            {
                return await _context.LastData.Find(_ => true).Limit(1).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddData(Lastdata item)
        {
            try
            {
                await _context.LastData.InsertOneAsync(item);
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
                return await _context.LastData.DeleteOneAsync(
                    Builders<Lastdata>.Filter.Eq("Guid", id));
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
                return await _context.LastData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
