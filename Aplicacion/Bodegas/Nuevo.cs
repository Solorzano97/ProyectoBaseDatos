using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Bodegas
{
    public class Nuevo
    {
        public class BodegaNueva : IRequest{

            
            [Required(ErrorMessage ="el campo nombre es requerido")]
            public string nombre {get; set;}

            [Required(ErrorMessage ="el campo direccion es requerido")]
            public string direccion {get;set;}

            [Required(ErrorMessage ="el campo telefono es requerido")]
            public string telefono {get; set;}

            [Required(ErrorMessage ="el campo email es requerido")]
            [EmailAddress(ErrorMessage = "No tiene el formato de email")]
            public string email {get;set;}

            
            
        }

        public class Manejador : IRequestHandler<BodegaNueva>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;
            }
            public async Task<Unit> Handle(BodegaNueva request, CancellationToken cancellationToken)
            {
                var bodegas = new bodega {
                    nombre = request.nombre ,  //aca el requeste jala los valores de la clase que cramos primero , la de BodegaNueva 
                    direccion = request.direccion , 
                    telefono = request.telefono , 
                    email = request.email
                };

                _context.bodega.Add(bodegas); //usar add para agregar data en esta caso de la variable bodega de arriba
                var valor = await _context.SaveChangesAsync(); //devolvera un valor ya sea 0 o 1 si es uno o mas si fue exitoso si no no
                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar la bodega");
                
            }
        }
    }
}