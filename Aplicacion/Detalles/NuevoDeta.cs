using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Detalle
{
    public class NuevoDeta
    {
        public class newdetalle : IRequest{

            [Required(ErrorMessage ="el campo cantidad es requerido")]
            [RegularExpression("[0-9]{1,5}", ErrorMessage = "Solo numeros")]
            
            [Range(0, 10000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 10000")]
            public int cantidad {get;set;}

            
            [Required(ErrorMessage ="el campo subtotal es requerido")]
             
            [Range(0, 100000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 100000")]
            public double subtotal {get;set;}

            public int ProductoId {get;set;}

            [Required(ErrorMessage ="el campo subtotal es requerido")]
            public int FacturaId {get;set;}
        }

        public class Manejador : IRequestHandler<newdetalle>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async  Task<Unit> Handle(newdetalle request, CancellationToken cancellationToken)
            {
                var det = new detalle{
                    cantidad = request.cantidad ,
                    subtotal = request.subtotal , 
                    ProductoId = request.ProductoId ,
                    FacturaId = request.FacturaId ,


                };

                 _context.detalle.Add(det);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el producto");
            }
        }


    }
}