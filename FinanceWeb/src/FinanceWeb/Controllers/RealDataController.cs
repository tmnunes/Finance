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
    public class RealDataController : Controller
    {
        private readonly IRealDataRepository _realDataRepository;

        public RealDataController(IRealDataRepository realDataRepository)
        {
            _realDataRepository = realDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]RealData jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _realDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<RealData> Get(string stock)
        {
            return _realDataRepository.GetDatabyStock(stock).Result;
        }
    }
}