using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.TipoMovimientos
{
    public class ConsultaTipoMov
    {
        public class tipomovlista : IRequest<List<tipoMovimiento>> {}

        public class Manejador : IRequestHandler<tipomovlista, List<tipoMovimiento>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<tipoMovimiento>> Handle(tipomovlista request, CancellationToken cancellationToken)
            {
                var tm = await _context.tipoMovimiento.ToListAsync();
                return tm;
            }
        }
    }
}