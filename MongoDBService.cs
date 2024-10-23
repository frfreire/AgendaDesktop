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
            //Cria uma conexão com o MongoDB, usando MongoClient
            var mongoCliente = new MongoClient("mongodb://localhost:27017");
            //Recupera do database um banco chamado agendaDB
            var mongoDatabase = mongoCliente.GetDatabase("agendaDB");
            //Passa para uma collection do tipo "Pessoa", uma collection chamada "pessoa"
            //que estava no MongoDBs
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

        public async Task AtualizarPessoaAsync(Pessoa pessoa)
        {
            await _pessoaCollection.ReplaceOneAsync(x => x.Id == pessoa.Id, pessoa);
        }

        public async Task ExcluirPessoaAsync(string id)
        {
            await _pessoaCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}