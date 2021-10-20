using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Clientes
{
    public class CosultaIdCli
    {
         public class clienteUnico : IRequest<cliente>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<clienteUnico, cliente>
        {
             private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<cliente> Handle(clienteUnico request, CancellationToken cancellationToken)
            {
                 var cli = await _context.cliente.FindAsync(request.Id);
                 if(cli == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontro el cliente"} );
                 }
                return cli;
            }
        }
    }
}