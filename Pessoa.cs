using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjetoAgenda
{
    public class Pessoa
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set;}
        public int Idade { get; set; }
    }
}