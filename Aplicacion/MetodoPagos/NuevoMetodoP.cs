using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.MetodoPagos
{
    public class NuevoMetodoP
    {
         public class newmetodopago : IRequest{

             [Required(ErrorMessage ="el campo nombre es requerido")]   
             [RegularExpression("[a-zA-Z]{2,20}", 
             ErrorMessage = "Solo admite    letras entre 2 y 20")]
             public string nombre {get;set;}
         }

        public class Manejador : IRequestHandler<newmetodopago>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newmetodopago request, CancellationToken cancellationToken)
            {
                 var metodos = new metodoPago{

                     nombre = request.nombre

                 };

                 _context.metodoPago.Add(metodos);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el metodopago");
            }
        }
    }
}