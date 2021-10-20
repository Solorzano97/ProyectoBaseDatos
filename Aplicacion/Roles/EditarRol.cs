using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Roles
{
    public class EditarRol
    {
        public class Editarrol : IRequest{

            public int RolId {get;set;}

            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")]
            public string Descripcion {get;set;}

        }

        public class Manejador : IRequestHandler<Editarrol>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarrol request, CancellationToken cancellationToken)
            {
                var roles = await _context.rol.FindAsync(request.RolId);
                  if(roles == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el rol"} );
                 }

                 roles.Descripcion = request.Descripcion ?? roles.Descripcion;
              

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}