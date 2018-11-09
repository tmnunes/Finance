using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceWeb.Models
{
    public class RealData
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Guid")]
        public string guid { get; set; }
        [BsonElement("Stock")]
        public string stock { get; set; }
        [BsonElement("Date")]
        public DateTime datetime { get; set; }
        [BsonElement("Ask")]
        public string ask { get; set; }
        [BsonElement("Bid")]
        public string bid { get; set; }
        [BsonElement("Open")]
        public string open { get; set; }
        [BsonElement("Close")]
        public string close { get; set; }  
        [BsonElement("Change")]
        public string change { get; set; }
    }
}
