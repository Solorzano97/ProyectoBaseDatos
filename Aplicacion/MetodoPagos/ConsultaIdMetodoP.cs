using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.MetodoPagos
{
    public class ConsultaIdMetodoP
    {
        public class metodoUnico : IRequest<metodoPago>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<metodoUnico, metodoPago>
        {
               private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<metodoPago> Handle(metodoUnico request, CancellationToken cancellationToken)
            {
               var metodos = await _context.metodoPago.FindAsync(request.Id);
                     if(metodos == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro metodo de pago"} );
                 }
                return metodos;
            }
        }
    }
}