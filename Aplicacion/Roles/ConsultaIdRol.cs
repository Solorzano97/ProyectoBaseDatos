using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Roles
{
    public class ConsultaIdRol
    {
        public class rolUnico : IRequest<rol>{

            public int Id {get;set;}
        }

        public class Manejador : IRequestHandler<rolUnico, rol>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<rol> Handle(rolUnico request, CancellationToken cancellationToken)
            {
                var roles = await _context.rol.FindAsync(request.Id);
                 if(roles == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el rol"} );
                 }
                return roles;
            }
        }
    }
}