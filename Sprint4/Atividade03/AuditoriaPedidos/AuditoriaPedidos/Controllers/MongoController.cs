using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MongoController : ControllerBase
    {
        IMongoCollection<Pedido> _collectionPedido;
        public MongoController()
        {
            string connectionString = "mongodb://localhost:27017";
            string databaseName = "mongo";
            string collectionName = "Pedido";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<Pedido>(collectionName);
            _collectionPedido = collection;
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] Pedido pedido)
        {
            _collectionPedido.InsertOneAsync(pedido);
            return Content(pedido.OrderId);
        }

        [HttpGet]
        [Route("SelectAll")]
        public Saida SelectAll()
        {
            var listaCompleta = _collectionPedido.Find(e => true);
            var saida = new Saida();
            var funcoes = new Funcoes();
            var contagem = listaCompleta.ToList().Count;
            saida.pageNumber = funcoes.PaginaAtual(contagem);
            saida.pageSize = 2;
            saida.pedidos = listaCompleta.ToList();
            return saida;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var result = _collectionPedido.DeleteOne(p => p.OrderId == id);
            if (result.DeletedCount > 0)
                return Ok();
            else
                return NotFound();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Pedido pedido)
        {
            var filter = Builders<Pedido>.Filter.Eq("Id", pedido.Id);
            var result = _collectionPedido.ReplaceOne(filter, pedido, new ReplaceOptions { IsUpsert = true });
            if (result.ModifiedCount  > 0)
                return Ok();
            else
                return NotFound();
        }
    }
}
