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
    public class ScoreController : Controller
    {
        private readonly IStockScoreDataRepository _stockScoreDataRepository;

        public ScoreController(IStockScoreDataRepository stockScoreDataRepository)
        {
            _stockScoreDataRepository = stockScoreDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]StockScoreData jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _stockScoreDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet]
        [AllowAnonymous]
        public StockScoreData GetFirst()
        {
            return _stockScoreDataRepository.GetFirst().Result;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public StockScoreData Get(string id)
        {
            return _stockScoreDataRepository.GetDatabyStock(id).Result;
        }

        [HttpGet("{id}/{date}")]
        [AllowAnonymous]
        public StockScoreData Get(string id,string date)
        {
            return _stockScoreDataRepository.GetDatabyStockDate(id, date).Result;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest();

            var product = _stockScoreDataRepository.GetData(id);
            product.Wait();
            var prodAux = product.Result;
            if (prodAux == null || prodAux.guid != id)
            {
                return NotFound();
            }

            var rescall = _stockScoreDataRepository.RemoveData(id);
            var res = rescall.Result;
            if (res.DeletedCount == 1)
                return new OkResult();
            else
                return new BadRequestResult();
        }
    }
}