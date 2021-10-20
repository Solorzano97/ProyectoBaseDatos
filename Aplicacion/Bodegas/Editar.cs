using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Bodegas
{
    public class Editar
    {
        public class EditarBodega : IRequest{

            public int BodegaId {get;set;}
            public string nombre {get; set;}
            public string direccion {get;set;}
            public string telefono {get; set;}
            [EmailAddress(ErrorMessage = "No tiene el formato de email")]
            public string email {get;set;}
        }

        public class Manejador : IRequestHandler<EditarBodega>{
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }

            public async Task<Unit> Handle(EditarBodega request, CancellationToken cancellationToken)
            {
                 var bodegas = await _context.bodega.FindAsync(request.BodegaId);
                 if(bodegas == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se econtro la bodega"} );
                 }

                 bodegas.nombre = request.nombre ?? bodegas.nombre; // por si no actualiza el nombre del curso se ponen??
                 bodegas.direccion = request.direccion ?? bodegas.direccion;
                 bodegas.telefono = request.telefono ?? bodegas.telefono;
                 bodegas.email = request.email ?? bodegas.email;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");

                
            }
        }
    }
}