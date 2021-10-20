using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Producto
{
    public class EditarPro
    {
        public class Editarproduct : IRequest{
            public int ProductoId {get; set;}

            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")]
            public string nombre {get; set;}
            public string descripcion {get; set;}
            public string imagen {get; set;}

            [Range(0, 10000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 10000")]
            public double precio {get; set;}

            [Range(0, 1000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 1000")]
            public double descuento {get; set;}
            public int CategoriaId {get; set;}
        }

        public class Manejador : IRequestHandler<Editarproduct>
        {
            
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }

            public async Task<Unit> Handle(Editarproduct request, CancellationToken cancellationToken)
            {
                 var prodductos = await _context.producto.FindAsync(request.ProductoId);
                   if(prodductos == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el producto"} );
                 }

                 prodductos.nombre= request.nombre ?? prodductos.nombre;
                 prodductos.descripcion = request.descripcion ?? prodductos.descripcion;
                 prodductos.imagen = request.imagen ?? prodductos.imagen;
                 prodductos.precio = request.precio ;
                 prodductos.descuento = request.descuento;
                 prodductos.CategoriaId = request.CategoriaId ;



              

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }

}