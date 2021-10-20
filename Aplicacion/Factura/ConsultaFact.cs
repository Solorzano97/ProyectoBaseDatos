using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;


namespace Aplicacion.Factura
{
    public class ConsultaFact
    {
        public class listafacturas : IRequest<List<factura>> {}

        public class Manejador : IRequestHandler<listafacturas, List<factura>>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<factura>> Handle(listafacturas request, CancellationToken cancellationToken)
            {
                var facts = await _context.factura.ToListAsync();
                return facts;
            }
        }
    }
}