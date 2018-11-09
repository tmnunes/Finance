using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceWeb.Models
{
    public class Lastdata
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("Guid")]
        public string guid { get; set; }
        [BsonElement("Symbol")]
        public string symbol { get; set; }
        [BsonElement("Date")]
        public string date { get; set; }
        [BsonElement("Week")]
        public string week { get; set; }
        [BsonElement("Month")]
        public string month { get; set; }
        [BsonElement("Year")]
        public string year { get; set; }
        [BsonElement("Close")]
        public string close { get; set; }
        [BsonElement("Low")]
        public string low { get; set; }
        [BsonElement("Open")]
        public string open { get; set; }
        [BsonElement("High")]
        public string high { get; set; }
        [BsonElement("SMA8")]
        public string SMA8 { get; set; }
        [BsonElement("SMA21")]
        public string SMA21 { get; set; }
        [BsonElement("SMA25")]
        public string SMA25 { get; set; }
        [BsonElement("SMA50")]
        public string SMA50 { get; set; }
        [BsonElement("SMA200")]
        public string SMA200 { get; set; }
        [BsonElement("SMA100")]
        public string SMA100 { get; set; }
        [BsonElement("EMA8")]
        public string EMA8 { get; set; }
        [BsonElement("EMA20")]
        public string EMA20 { get; set; }
        [BsonElement("EMA50")]
        public string EMA50 { get; set; }
        [BsonElement("EMA200")]
        public string EMA200 { get; set; }
        [BsonElement("RSI14")]
        public string RSI14 { get; set; }
        [BsonElement("MACD")]
        public string MACD { get; set; }
        [BsonElement("ADLVolume")]
        public string ADLVolume { get; set; }
        [BsonElement("STDDEV25")]
        public string STDDEV25 { get; set; }
        [BsonElement("STDDEV100")]
        public string STDDEV100 { get; set; }
    }
}
