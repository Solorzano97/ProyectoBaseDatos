using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Factura
{
    public class ConsultaIdFact
    {
         public class facturaUnica : IRequest<factura>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<facturaUnica, factura>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<factura> Handle(facturaUnica request, CancellationToken cancellationToken)
            {
                var fact = await _context.factura.FindAsync(request.Id);
                 if(fact == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro factura"} );
                 }

                return fact;
            }
        }

    }
}