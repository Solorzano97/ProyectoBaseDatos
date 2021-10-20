using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Movimiento
{
    public class ConsultaMov
    {
        public class movlista : IRequest<List<movimientos>> {}

        public class Manejador : IRequestHandler<movlista, List<movimientos>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<movimientos>> Handle(movlista request, CancellationToken cancellationToken)
            {
                var movimiento = await _context.movimientos.ToListAsync();
                return movimiento;
            }
        }
    }
}