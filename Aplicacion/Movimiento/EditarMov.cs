using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Movimiento
{
    public class EditarMov
    {
        public class Editarmovimiento : IRequest{
            public int MovimientosId {get;set;}
            
            [RegularExpression("[0-9]{1,4}", ErrorMessage = "Solo numeros")]
            
            [Range(0, 1000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 1000")]
            public int cantidad {get;set;}
            public int BodegaId {get;set;}
            public int ProductoId {get;set;}
            public int TipoMovimientoId {get;set;}
        }


        public class Manejador : IRequestHandler<Editarmovimiento>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarmovimiento request, CancellationToken cancellationToken)
            {
                var movi = await _context.movimientos.FindAsync(request.MovimientosId);
                     if(movi == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro tipo de movimiento"} );
                 }

                 movi.cantidad = request.cantidad;
                 movi.BodegaId = request.BodegaId ;
                 movi.ProductoId = request.ProductoId ;
                 movi.TipoMovimientoId = request.TipoMovimientoId;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}