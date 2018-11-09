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
    public class AnnualDataController : Controller
    {
        private readonly IAnnualDataRepository _annualDataRepository;

        public AnnualDataController(IAnnualDataRepository annualDataRepository)
        {
            _annualDataRepository = annualDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]AnnualData jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _annualDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet("{stock}")]
        [AllowAnonymous]
        public IEnumerable<AnnualData> Get(string stock)
        {
            return _annualDataRepository.GetDatabyStock(stock).Result;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IEnumerable<StockData> Get()
        //{
        //    return _stockDataRepository.GetAllData().Result;
        //}
    }
}