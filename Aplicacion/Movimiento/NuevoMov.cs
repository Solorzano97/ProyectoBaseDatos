using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Movimiento
{
    public class NuevoMov
    {
        public class newmovimiento : IRequest{

            [Required(ErrorMessage ="el campo cantidad es requerido")]
            [RegularExpression("[0-9]{1,4}", ErrorMessage = "Solo numeros")]
            
            [Range(0, 1000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 1000")]
            public int cantidad {get;set;}
            public int BodegaId {get;set;}
            public int ProductoId {get;set;}
            public int TipoMovimientoId {get;set;}
            public int BodegaId2 {get;set;} 
        }

        public class Manejador : IRequestHandler<newmovimiento>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newmovimiento request, CancellationToken cancellationToken)
            {
                var movimiento = new movimientos{

                    cantidad = request.cantidad ,
                    BodegaId = request.BodegaId ,
                    ProductoId = request.ProductoId ,
                    TipoMovimientoId = request.TipoMovimientoId ,
                    BodegaId2 = request.BodegaId2 
            };

            _context.movimientos.Add(movimiento);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el producto");
        }

    }
}
}