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
    public class ExecutionController : Controller
    {
        private readonly IExecutionRepository _executionRepository;

        public ExecutionController(IExecutionRepository executionRepository)
        {
            _executionRepository = executionRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Post([FromBody]Execution jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _executionRepository.AddExecution(jdata).Wait();
            }
            return true;
        }

        [HttpGet("{process}")]
        [AllowAnonymous]
        public Execution Get(string process)
        {
            if (process == "Main")
            {
                List<bool> Lbool = new List<bool>();
                string let = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                foreach (char l in let)
                {
                    var l1 = _executionRepository.GetExecutionbyprocess("ThSeasonality" + l);
                    l1.Wait();
                    if (l1.Result != null)
                        Lbool.Add(l1.Result.isRunning);

                    var l2 = _executionRepository.GetExecutionbyprocess("ThScore" + l);
                    l2.Wait();
                    if (l2.Result != null)
                        Lbool.Add(l2.Result.isRunning);

                    var l3 = _executionRepository.GetExecutionbyprocess("ThReal" + l);
                    l3.Wait();
                    if (l3.Result != null)
                        Lbool.Add(l3.Result.isRunning);
                }

                var m1 = _executionRepository.GetExecutionbyprocess("Main");
                m1.Wait();
                if (m1.Result != null)
                {
                    Execution ret = m1.Result;
                    if (Lbool.Exists(c => c == true))
                        ret.isRunning = true;
                    else
                        ret.isRunning = false;
                    return ret;
                }
                else
                {
                    return null;
                }
            }

            return _executionRepository.GetExecutionbyprocess(process).Result;
        }

        [HttpPut("{process}")]
        [AllowAnonymous]
        public async Task<bool> Put(string process, [FromBody]Execution jdata)
        {
            if (ModelState.IsValid && jdata != null)
            {
                _executionRepository.UpdateExecution(process, jdata).Wait();
            }
            return true;
        }
    }
}