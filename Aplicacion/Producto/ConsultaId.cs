using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Producto
{
    public class ConsultaId
    {
         public class productUnico : IRequest<producto>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<productUnico, producto>
        {
                private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<producto> Handle(productUnico request, CancellationToken cancellationToken)
            {
                var productos = await _context.producto.FindAsync(request.Id);
                 if(productos == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el producto"} );
                 }
                return productos;
            }
        }
    }
}