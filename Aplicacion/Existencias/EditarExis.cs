using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Existencias
{
    public class EditarExis
    {
         public class Editarexistencia : IRequest{
             public int ExistenciasId {get;set;}

              
            [RegularExpression("[0-9]{1,5}", ErrorMessage = "Solo numeros")]
            
            [Range(0, 10000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 10000")]
            public int cantidad {get;set;}
            public int ProductoId {get;set;}
            public int ProveedorId {get;set;}
            public int BodegaId {get;set;}
         }

        public class Manejador : IRequestHandler<Editarexistencia>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarexistencia request, CancellationToken cancellationToken)
            {
                 var exist = await _context.existencias.FindAsync(request.ExistenciasId);
                 if(exist == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontraron existencias"} );
                 }

                 
                 exist.cantidad = request.cantidad;
                 exist.ProductoId = request.ProductoId;
                 exist.ProveedorId = request.ProveedorId;
                 exist.BodegaId = request.BodegaId;
                 
                  var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }

    }
}