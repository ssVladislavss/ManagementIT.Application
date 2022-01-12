using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;
using ManagementIt.Core.ResponseModels;
using MongoDB.Bson;

namespace ManagementIt.Core.Domain.MongoEntity
{
    public class LogMessage
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string DateOrTime { get; set; }
        public string Iniciator { get; set; }

        public LogMessage(){}

        public LogMessage(string address, string message, NotificationType type, string iniciator)
        {
            Address = address;
            Message = message;
            Type = type.ToString();
            DateOrTime = DateTime.Now.ToString("F");
            Iniciator = iniciator;
        }

        public static LogMessage GetLogMessage(string address, string message, NotificationType type, string iniciator = "") =>
            new LogMessage(address, message, type, iniciator);
    }
}
