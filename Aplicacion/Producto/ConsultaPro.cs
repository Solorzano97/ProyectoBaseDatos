using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Producto
{
    public class ConsultaPro
    {
         public class productolista : IRequest<List<producto>> {}

        public class Manejador : IRequestHandler<productolista, List<producto>>
        {
             private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<producto>> Handle(productolista request, CancellationToken cancellationToken)
            {
                var productos = await _context.producto.ToListAsync();
                return productos;
            }
        }
    }
}