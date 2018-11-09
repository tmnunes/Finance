using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceWeb.Models
{
    public class StockScoreData
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Guid")]
        public string guid { get; set; }
        [BsonElement("Stock")]
        public string stock { get; set; }
        [BsonElement("Date")]
        public DateTime datetime { get; set; }
        [BsonElement("JData")]
        public string jsondata { get; set; }
    }
}
