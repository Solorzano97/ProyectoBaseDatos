using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Existencias
{
    public class ConsultaExis
    {
        public class listaexistencias : IRequest<List<existencias>> {}

        public class Manejador : IRequestHandler<listaexistencias, List<existencias>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<existencias>> Handle(listaexistencias request, CancellationToken cancellationToken)
            {
                var exist = await _context.existencias.ToListAsync();
                return exist;
            }
        }

    }
}