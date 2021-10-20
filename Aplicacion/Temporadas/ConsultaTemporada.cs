
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Temporadas
{
    public class ConsultaTemporada
    {
         public class temporadalista : IRequest<List<temporada>> {}

        public class Manejador : IRequestHandler<temporadalista, List<temporada>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<temporada>> Handle(temporadalista request, CancellationToken cancellationToken)
            {
                var t = await _context.temporada.ToListAsync();
                return t;
            }
        }
    }
}