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
    [Authorize(Roles = "ad")]
    [Route("api/[controller]")]
    public class SeasonalityController : Controller
    {
        private readonly IStockSeasonalityDataRepository _stockSeasonalityDataRepository;

        public SeasonalityController(IStockSeasonalityDataRepository stockSeasonalityDataRepository)
        {
            _stockSeasonalityDataRepository = stockSeasonalityDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]StockSeasonalityData jdata)
        {

            if (ModelState.IsValid && jdata != null)
            {
                //StockSeasonalityData data = JsonConvert.DeserializeObject<StockSeasonalityData>(jdata);
                _stockSeasonalityDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet]
        [AllowAnonymous]
        public StockSeasonalityData GetFirst(string returnUrl = null)
        {
            return _stockSeasonalityDataRepository.GetFirst().Result;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest();

            var product = _stockSeasonalityDataRepository.GetData(id);
            product.Wait();
            var prodAux = product.Result;
            if (prodAux == null || prodAux.guid != id)
            {
                return NotFound();
            }

            var rescall = _stockSeasonalityDataRepository.RemoveData(id);
            var res = rescall.Result;
            if (res.DeletedCount == 1)
                return new OkResult();
            else
                return new BadRequestResult();
        }
    }
}