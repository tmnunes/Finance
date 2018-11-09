using FinanceWeb.Models;
using MongoDB.Driver;
using FinanceWeb.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinanceWeb.Controllers
{
    public class DBStatusController : Controller
    {
        private readonly DataContext _context = null;

        public DBStatusController(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public IActionResult Index()
        {
            Stats newStats = new Stats();
            newStats.AnnualCount = _context.AnnualData.Find(_ => true).Count();
            newStats.FutureCount = _context.FutureData.Find(_ => true).Count();
            newStats.PastCount = _context.PastData.Find(_ => true).Count();
            newStats.RealCount = _context.RealData.Find(_ => true).Count();
            newStats.ScoreDataCount = _context.StockScoreData.Find(_ => true).Count();
            newStats.SeasonalityDataCount = _context.StockSeasonalityData.Find(_ => true).Count();
            newStats.StockDataCount = _context.StockData.Find(_ => true).Count();
            newStats.StockCount = _context.StockSymbols.Find(_ => true).Count();  

            return View(newStats);
        }

    }
}