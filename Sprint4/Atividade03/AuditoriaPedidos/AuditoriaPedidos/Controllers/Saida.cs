using System.Collections.Generic;

namespace MongoDb.Controllers
{
    public class Saida
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public List<Pedido> pedidos{ get; set;}
    }
}
