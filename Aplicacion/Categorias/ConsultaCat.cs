using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Categorias
{
    public class ConsultaCat
    {
        public class categorialista : IRequest<List<categoria>> {}

        public class Manejador : IRequestHandler<categorialista, List<categoria>>
        {
               private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<categoria>> Handle(categorialista request, CancellationToken cancellationToken)
            {
                var categorias = await _context.categoria.ToListAsync();
                return categorias;
            }
        }

    }
}