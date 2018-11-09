using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceWeb.Models
{
    // Data from ScoreData + Value from Seasonality
    public class PastData
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Guid")]
        public string guid { get; set; }
        [BsonElement("Stock")]
        public string stock { get; set; }
        [BsonElement("Date")]
        public DateTime date { get; set; }
        [BsonElement("Value")]
        public string value { get; set; }
        [BsonElement("Score")]
        public string score { get; set; }
        [BsonElement("Signal")]
        public string signal { get; set; }
        [BsonElement("ScoreMedian")]
        public string scoreMedian { get; set; }
        [BsonElement("MACD")]
        public string macd { get; set; }
        [BsonElement("RSI14")]
        public string rsi14 { get; set; }
        [BsonElement("ADL")]
        public string adl { get; set; }
        [BsonElement("ChartMessage")]
        public List<string> chartmessage { get; set; }
        [BsonElement("TimeStamp")]
        public DateTime timestamp { get; set; }
    }
}
