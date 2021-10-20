using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Detalles
{
    public class ConsultaDeta
    {

        public class listadetalles : IRequest<List<detalle>> {}

        public class Manejador : IRequestHandler<listadetalles, List<detalle>>
        {
             private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<detalle>> Handle(listadetalles request, CancellationToken cancellationToken)
            {
                var deta = await _context.detalle.ToListAsync();
                return deta;
            }
        }

    }
}