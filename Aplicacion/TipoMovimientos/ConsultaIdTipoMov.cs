using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.TipoMovimientos
{
    public class ConsultaIdTipoMov
    {
         public class Unico : IRequest<tipoMovimiento>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<Unico, tipoMovimiento>
        {

             private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public  async Task<tipoMovimiento> Handle(Unico request, CancellationToken cancellationToken)
            {
                var tm = await _context.tipoMovimiento.FindAsync(request.Id);
                   if(tm == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el tipo de movimiento"} );
                 }
                return tm;
            }
        }
    }
}