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
    public class StockSymbolRepository : IStockSymbolRepository
    {
        private readonly DataContext _context = null;

        public StockSymbolRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<IEnumerable<StockSymbol>> GetAllStockSymbols()
        {
            try
            {
                return await _context.StockSymbols.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<StockSymbol> GetStockSymbol(string id)
        {
            var filter = Builders<StockSymbol>.Filter.Eq("Guid", id);

            try
            {
                return await _context.StockSymbols
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<List<StockSymbol>> GetStockSymbolbySymbol(string name)
        {
            var filter = Builders<StockSymbol>.Filter.Eq("symbol", name);
            //var sortBy = Builders<StockSymbol>.Sort.Descending("Id");

            try
            {
                return await _context.StockSymbols
                    .Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddStockSymbol(StockSymbol item)
        {
            try
            {
                await _context.StockSymbols.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveStockSymbol(string id)
        {
            try
            {
                return await _context.StockSymbols.DeleteOneAsync(
                    Builders<StockSymbol>.Filter.Eq("Guid", id));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UpdateResult> UpdateStockSymbol(string id, string body)
        {
            var filter = Builders<StockSymbol>.Filter.Eq(s => s.guid, id);
            var update = Builders<StockSymbol>.Update
                .Set(s => s.name, body);
            //.CurrentDate(s => s.UpdatedOn);

            try
            {
                return await _context.StockSymbols.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<ReplaceOneResult> UpdateStockSymbol(string id, StockSymbol item)
        {
            try
            {
                return await _context.StockSymbols
                    .ReplaceOneAsync(n => n.guid.Equals(id)
                        , item
                        , new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<ReplaceOneResult> UpdateStockSymbolDocument(string id, string body)
        {
            var item = await GetStockSymbol(id) ?? new StockSymbol();
            item.name = body;
            //item.UpdatedOn = DateTime.Now;

            return await UpdateStockSymbol(id, item);
        }

        public async Task<DeleteResult> RemoveAllStockSymbol()
        {
            try
            {
                return await _context.StockSymbols.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
