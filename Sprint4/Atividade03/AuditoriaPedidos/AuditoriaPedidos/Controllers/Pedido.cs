using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MongoDb.Controllers
{
    public class Pedido
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public string Api { get; set; }
        public InformacoesAdicionais Content { get; set; }
    }
}