using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.MetodoPagos
{
    public class EditarMetodoP
    {
         public class Editarmetodopago : IRequest{

             public int MetodoPagoId {get;set;}

                
             [RegularExpression("[a-zA-Z]{2,20}", 
             ErrorMessage = "Solo admite    letras entre 2 y 20")]
             public string nombre {get;set;}

         }

        public class Manejador : IRequestHandler<Editarmetodopago>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarmetodopago request, CancellationToken cancellationToken)
            {
                var metodos = await _context.metodoPago.FindAsync(request.MetodoPagoId);
                    if(metodos == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro metodo de pago"} );
                 }

                 
                 metodos.nombre = request.nombre ?? metodos.nombre;

                  var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }


    }
}