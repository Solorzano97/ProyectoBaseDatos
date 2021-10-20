using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;


namespace Aplicacion.MetodoPagos
{
    public class ConsultaMetodoP
    {
         public class metodopagolista : IRequest<List<metodoPago>> {}

        public class Manejador : IRequestHandler<metodopagolista, List<metodoPago>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<metodoPago>> Handle(metodopagolista request, CancellationToken cancellationToken)
            {
                var metodos = await _context.metodoPago.ToListAsync();
                return metodos;
            }
        }
    }
}