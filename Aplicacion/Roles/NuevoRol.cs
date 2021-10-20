using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Roles
{
    public class NuevoRol
    {

        public class RolaNuevo : IRequest{


            [Required(ErrorMessage ="el campo descripcion es requerido")]   
        [RegularExpression("[a-zA-Z]{2,20}", 
        ErrorMessage = "Solo admite    letras entre 2 y 20")]
            public string Descripcion {get;set;}

        }

        public class Manejador : IRequestHandler<RolaNuevo>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;
            }
            public async Task<Unit> Handle(RolaNuevo request, CancellationToken cancellationToken)
            {
                var roles = new rol {
                    Descripcion = request.Descripcion //aca el requeste jala los valores de la clase que cramos primero , la de BodegaNueva 
                    
                };

                _context.rol.Add(roles); //usar add para agregar data en esta caso de la variable bodega de arriba
                var valor = await _context.SaveChangesAsync(); //devolvera un valor ya sea 0 o 1 si es uno o mas si fue exitoso si no no
                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el nuevo rol");
                
            }
        }

    }
}