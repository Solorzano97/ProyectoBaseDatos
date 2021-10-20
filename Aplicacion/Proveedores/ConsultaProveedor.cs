using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;


namespace Aplicacion.Proveedores
{
    public class ConsultaProveedor
    {
        public class proveedorlista : IRequest<List<proveedor>> {}

        public class Manejador : IRequestHandler<proveedorlista, List<proveedor>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<proveedor>> Handle(proveedorlista request, CancellationToken cancellationToken)
            {
                var pro = await _context.proveedor.ToListAsync();
                return pro;
            }
        }
    }
}