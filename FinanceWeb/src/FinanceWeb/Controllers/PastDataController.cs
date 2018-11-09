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
    public class PastDataController : Controller
    {
        private readonly IPastDataRepository _pastDataRepository;

        public PastDataController(IPastDataRepository pastDataRepository)
        {
            _pastDataRepository = pastDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]PastData jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _pastDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<PastData> Get(string stock)
        {
            return _pastDataRepository.GetDatabyStock(stock).Result;
        }

        [HttpGet("{stock}")]
        [AllowAnonymous]
        public PastData GetPastbyStockDes(string stock)
        {
            return _pastDataRepository.GetPastbyStockDes(stock).Result;
        }
    }
}