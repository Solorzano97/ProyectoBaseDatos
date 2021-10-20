using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Movimiento
{
    public class ConsultaIdMov
    {
         public class movUnico : IRequest<movimientos>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<movUnico, movimientos>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<movimientos> Handle(movUnico request, CancellationToken cancellationToken)
            {
                 var movi = await _context.movimientos.FindAsync(request.Id);
                   if(movi == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro tipo de movimiento"} );
                 }
                return movi;
            }
        }
    }
}