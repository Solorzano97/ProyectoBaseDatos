using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Detalles
{
    public class EditarDeta
    {
        public class Editardetalle : IRequest{
            public int DetalleId {get; set;}

            [RegularExpression("[0-9]{1,5}", ErrorMessage = "Solo numeros")]
            
            [Range(0, 10000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 10000")]
            public int cantidad {get;set;}

            [Range(0, 100000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 100000")]
            public double subtotal {get;set;}

            public int ProductoId {get;set;}
            public int FacturaId {get;set;}

        }

        public class Manejador : IRequestHandler<Editardetalle>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editardetalle request, CancellationToken cancellationToken)
            {
                var deta = await _context.detalle.FindAsync(request.DetalleId);
                 if(deta == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontro el detalle"} );
                 }

                 
                 deta.cantidad = request.cantidad;
                 deta.subtotal = request.subtotal ;
                 deta.ProductoId = request.ProductoId ;
                 deta.FacturaId = request.FacturaId ;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}