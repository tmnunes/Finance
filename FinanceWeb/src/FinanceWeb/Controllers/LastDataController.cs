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
    public class LastDataController : Controller
    {
        private readonly IStockLastDataRepository _stockLastDataRepository;

        public LastDataController(IStockLastDataRepository stockLastDataRepository)
        {
            _stockLastDataRepository = stockLastDataRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]Lastdata jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _stockLastDataRepository.AddData(jdata).Wait();
            }
            return true;
        }

        [HttpGet]
        [AllowAnonymous]
        public Lastdata GetFirst()
        {
            return _stockLastDataRepository.GetFirst().Result;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public Lastdata Get(string id)
        {
            return _stockLastDataRepository.GetDatabyStock(id).Result;
        }

        [HttpGet("{id}/{date}")]
        [AllowAnonymous]
        public Lastdata Get(string id,string date)
        {
            return _stockLastDataRepository.GetDatabyStockDate(id, date).Result;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
                return BadRequest();

            var product = _stockLastDataRepository.GetData(id);
            product.Wait();
            var prodAux = product.Result;
            if (prodAux == null || prodAux.guid != id)
            {
                return NotFound();
            }

            var rescall = _stockLastDataRepository.RemoveData(id);
            var res = rescall.Result;
            if (res.DeletedCount == 1)
                return new OkResult();
            else
                return new BadRequestResult();
        }
    }
}