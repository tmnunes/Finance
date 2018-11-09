using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceWeb.Models
{
    public class Execution
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Guid")]
        public string guid { get; set; }
        [BsonElement("Process")]
        public string process { get; set; }
        [BsonElement("Date")]
        public DateTime datetime { get; set; }
        [BsonElement("IsActive")]
        public bool isActive { get; set; }
        [BsonElement("IsComplete")]
        public bool isComplete { get; set; }
        [BsonElement("IsRunning")]
        public bool isRunning { get; set; }
    }
}
