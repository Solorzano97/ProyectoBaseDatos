using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Existencias
{
    public class NuevaExis
    {
        public class newexist : IRequest{


             [Required(ErrorMessage ="el campo cantidad es requerido")]
            [RegularExpression("[0-9]{1,5}", ErrorMessage = "Solo numeros")]
            
            [Range(0, 10000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 10000")]            
            public int cantidad {get;set;}
            public int ProductoId {get;set;}
            public int ProveedorId {get;set;}
            public int BodegaId {get;set;}
        }

        public class Manejador : IRequestHandler<newexist>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newexist request, CancellationToken cancellationToken)
            {
                 var exist = new existencias{

                     cantidad = request.cantidad ,
                     ProductoId = request.ProductoId ,
                     ProveedorId = request.ProveedorId ,
                     BodegaId = request.BodegaId 

            };
            _context.existencias.Add(exist);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar la existencia");
        }
    }
}
}