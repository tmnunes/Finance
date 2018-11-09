using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceWeb.Models
{
    // Data from Seasonality
    public class AnnualData
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Guid")]
        public string guid { get; set; }
        [BsonElement("Stock")]
        public string stock { get; set; }
        [BsonElement("Year")]
        public string year { get; set; }
        [BsonElement("Value")]
        public string value { get; set; }
        [BsonElement("TimeStamp")]
        public DateTime timestamp { get; set; }
    }
}
