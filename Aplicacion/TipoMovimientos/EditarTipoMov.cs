using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.TipoMovimientos
{
    public class EditarTipoMov
    {
        public class Editartipomovimiento : IRequest{
            public int TipoMovimientoId {get;set;}
        public string descripcion {get;set;}
        }

        public class Manejador : IRequestHandler<Editartipomovimiento>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editartipomovimiento request, CancellationToken cancellationToken)
            {
                 var tm = await _context.tipoMovimiento.FindAsync(request.TipoMovimientoId);
                   if(tm == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el tipo de movimiento"} );
                 }

                 tm.descripcion = request.descripcion ?? tm.descripcion;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }


    }
}