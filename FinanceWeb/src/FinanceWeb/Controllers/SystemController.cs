using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceWeb.Interfaces;
using FinanceWeb.Models;
using FinanceWeb.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace FinanceWeb.Controllers
{
    [Authorize(Roles = "ad")]
    //[Authorize]
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly IStockSymbolRepository _stocksymbolRepository;

        public SystemController(IStockSymbolRepository stocksymbolRepository)
        {
            _stocksymbolRepository = stocksymbolRepository;
        }

        // Call an initialization - api/system/initstock
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "initstock")
            {
                _stocksymbolRepository.RemoveAllStockSymbol();

                for (char c = 'A'; c <= 'Z'; c++)
                {
                    GetData.LoadStocksFinanchill(c.ToString()).Wait();
                }

                foreach (var stock in GetData._stocks)
                {
                    Task<List<StockSymbol>> list = _stocksymbolRepository.GetStockSymbolbySymbol(stock.symbol);

                    if (list.Result.Count == 0)
                        _stocksymbolRepository.AddStockSymbol(new StockSymbol() { _id = ObjectId.GenerateNewId(), guid = Guid.NewGuid().ToString(), name = stock.name, symbol = stock.symbol });
                    else
                        GetData._stocks_off.Add(stock);
                }

                return "Done: Get:" + GetData._stocks.Count + "   Not Inserted:" + GetData._stocks_off.Count;
            }

            return "Unknown";
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<StockSymbol> Get()
        {
            var t = _stocksymbolRepository.GetAllStockSymbols().Result;
            return t;
        }
    }
}