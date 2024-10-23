using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ProjetoAgenda
{
    public class MongoDBService
    {

        private readonly IMongoCollection<Pessoa> _pessoaCollection;

        public MongoDBService()
        {
            var mongoCliente = new MongoClient("mongodb://localhost:27017");
            var mongoDatabase = mongoCliente.GetDatabase("agendaDB");
            _pessoaCollection = mongoDatabase.GetCollection<Pessoa>("pessoa");
        }
        
        public async Task<List<Pessoa>> GetPessoasAsync()
        {
            return await _pessoaCollection.Find(_ => true).ToListAsync();
        }
        
        public async Task<Pessoa> GetPessoaByIdAsync(string id)
        {
            return await _pessoaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CriarPessoaAsync(Pessoa pessoa)
        {
            await _pessoaCollection.InsertOneAsync(pessoa);
        }
    }
}