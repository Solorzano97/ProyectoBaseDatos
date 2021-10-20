using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Producto
{
    public class NuevoProduct
    {
        public class newproducto : IRequest{
            

            [Required(ErrorMessage ="el campo nombre es requerido")]   
            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")]
            public string nombre {get; set;}

            [Required(ErrorMessage ="el campo descripcion es requerido")]   
            public string descripcion {get; set;}
            public string imagen {get; set;}

            [Required(ErrorMessage ="el campo precio es requerido")]
            [Range(0, 10000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 10000")]
            public double precio {get; set;}

            
            [Range(0, 1000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 1000")]
            public double descuento {get; set;}
            public int CategoriaId {get; set;}
        }

        public class Manejador : IRequestHandler<newproducto>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newproducto request, CancellationToken cancellationToken)
            {
                var productos = new producto{

                    nombre = request.nombre ,
                    descripcion = request.descripcion , 
                    imagen = request.imagen , 
                    precio = request.precio , 
                    descuento = request.descuento , 
                    CategoriaId = request.CategoriaId

                };

                _context.producto.Add(productos);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el producto");
            }
        }
    }
}