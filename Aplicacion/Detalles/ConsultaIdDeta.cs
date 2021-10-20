using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Detalles
{
    public class ConsultaIdDeta
    {
        public class detalleUnico : IRequest<detalle>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<detalleUnico, detalle>
        {
                private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<detalle> Handle(detalleUnico request, CancellationToken cancellationToken)
            {
               var deta = await _context.detalle.FindAsync(request.Id);
                if(deta == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontro el detalle"} );
                 }
                return deta;
            }
        }

    }
}