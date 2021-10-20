using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Clientes
{
    public class ConsultaCliente
    {
        public class clienteslista : IRequest<List<cliente>> {}

        public class Manejador : IRequestHandler<clienteslista, List<cliente>>
        {
             private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<cliente>> Handle(clienteslista request, CancellationToken cancellationToken)
            {
                var cli = await _context.cliente.ToListAsync();
                return cli;
            }
        }
    }
}