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
    public class StockController : Controller
    {
        private readonly IStockDataRepository _stockDataRepository;

        public StockController(IStockDataRepository stockDataRepository)
        {
            _stockDataRepository = stockDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]StockData jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _stockDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet]
        [AllowAnonymous]
        public StockData GetFirst(string returnUrl = null)
		{
            return _stockDataRepository.GetFirst().Result;
		}

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest();

            var product = _stockDataRepository.GetData(id);
            product.Wait();
            var prodAux = product.Result;
            if (prodAux == null || prodAux.guid != id)
            {
                return NotFound();
            }

            var rescall= _stockDataRepository.RemoveData(id);
            var res = rescall.Result;
            if (res.DeletedCount == 1)
                return new OkResult();
            else
                return new BadRequestResult();
        }

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Put(string id, [FromBody]Product p)
        //{
        //    var recId = new ObjectId(id);
        //    var product = objds.GetProduct(recId);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    objds.Update(recId, p);
        //    return new OkResult();
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IEnumerable<StockData> Get()
        //{
        //    return _stockDataRepository.GetAllData().Result;
        //}
    }
}