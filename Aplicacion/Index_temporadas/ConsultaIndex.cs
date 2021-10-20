using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Index_temporadas
{
    public class ConsultaIndex
    {
        public class listaindextemp : IRequest<List<indexTemporada>> {}

        public class Manejador : IRequestHandler<listaindextemp, List<indexTemporada>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<indexTemporada>> Handle(listaindextemp request, CancellationToken cancellationToken)
            {
                var index = await _context.indexTemporada.ToListAsync();
                   
                return index;
            }
        }

    }
}