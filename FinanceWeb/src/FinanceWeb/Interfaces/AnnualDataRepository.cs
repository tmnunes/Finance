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
    public class AnnualDataRepository : IAnnualDataRepository
    {
        private readonly DataContext _context = null;

        public AnnualDataRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public long Count()
        {
            try
            {
                return _context.AnnualData.Find(_ => true).Count();
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }

        public async Task<IEnumerable<AnnualData>> GetAllData()
        {
            try
            {
                return await _context.AnnualData.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<AnnualData> GetData(string id)
        {
            var filter = Builders<AnnualData>.Filter.Eq("Guid", id);

            try
            {
                return await _context.AnnualData
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<AnnualData>> GetDatabyStock(string stock)
        {
            var filter = Builders<AnnualData>.Filter.Eq("Stock", stock);

            try
            {
                return await _context.AnnualData.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddData(AnnualData item)
        {
            try
            {
                await _context.AnnualData.InsertOneAsync(item);
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
                return await _context.AnnualData.DeleteOneAsync(
                    Builders<AnnualData>.Filter.Eq("Guid", id));
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
                return await _context.AnnualData.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
