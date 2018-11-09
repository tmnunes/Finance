using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceWeb.Models
{
    // Data from Seasonality
    public class FutureData
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Guid")]
        public string guid { get; set; }
        [BsonElement("Stock")]
        public string stock { get; set; }
        [BsonElement("Date")]
        public DateTime datetime { get; set; }
        [BsonElement("Value")]
        public string value { get; set; }
        [BsonElement("HistProbability")]
        public string histprobability { get; set; }
        [BsonElement("TimeStamp")]
        public DateTime timestamp { get; set; }
    }
}
