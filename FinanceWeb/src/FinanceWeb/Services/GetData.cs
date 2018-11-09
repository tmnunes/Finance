using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using FinanceWeb.Controllers;
using FinanceWeb.Interfaces;
using FinanceWeb.Models;
using Microsoft.Extensions.Options;

namespace FinanceWeb.Services
{
    public class GetData
    {
        public static List<SSymbol> _stocks = new List<SSymbol>();

        public static List<SSymbol> _stocks_off = new List<SSymbol>();

        public static async Task LoadStocksFinanchill(string args)
        {
            // encoder
            UTF8Encoding enc = new UTF8Encoding();

            WebRequest request = WebRequest.Create("https://financhill.com/symbol/" + args + ".json");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.Credentials = new NetworkCredential(user, secret);
            //Stream dataStream = await request.GetRequestStreamAsync();
            //dataStream.Write(enc.GetBytes(data), 0, data.Length);

            WebResponse wr = await request.GetResponseAsync();
            Stream receiveStream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader.ReadToEnd();
                
            object objResponse = JsonConvert.DeserializeObject<List<SSymbol>>(content);

            List<SSymbol> collection = (List<SSymbol>)objResponse;

            foreach (SSymbol obj in collection)
            {
                _stocks.Add(obj);
            }
        }
    }

    public class SSymbol
    {
        public string symbol { get; set; }
        public string name { get; set; }
    }
}
