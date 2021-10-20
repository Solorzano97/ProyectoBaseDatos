using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Bodega
{
    public class Consulta
    {
        public class bodegalista : IRequest<List<bodega>> {}

        public class Manejador : IRequestHandler<bodegalista, List<bodega>>
        {

            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<bodega>> Handle(bodegalista request, CancellationToken cancellationToken)
            {
                var bodegas = await _context.bodega.ToListAsync();
                return bodegas;
            }
        }
    }
}