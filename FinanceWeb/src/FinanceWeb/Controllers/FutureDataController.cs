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
using MongoDB.Bson.IO;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace FinanceWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FutureDataController : Controller
    {
        private readonly IFutureDataRepository _futureDataRepository;

        public FutureDataController(IFutureDataRepository futureDataRepository)
        {
            _futureDataRepository = futureDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]FutureData jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _futureDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<FutureData> Get(string stock)
        {
            return _futureDataRepository.GetDatabyStock(stock).Result;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IEnumerable<StockData> Get()
        //{
        //    return _stockDataRepository.GetAllData().Result;
        //}
    }
}