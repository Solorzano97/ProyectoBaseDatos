using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Roles
{
    public class ConsultaRol
    {

        public class rollista : IRequest<List<rol>>{}

        public class Manejador : IRequestHandler<rollista, List<rol>>
        {
             private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<rol>> Handle(rollista request, CancellationToken cancellationToken)
            {
                var roles = await _context.rol.ToListAsync();
                return roles;
                
            }
        }

    }
}