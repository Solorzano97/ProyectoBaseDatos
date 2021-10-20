using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Empleados
{
    public class ConsultaEmp
    {
        public class empleadolista : IRequest<List<empleado>> {}

        public class Manejador : IRequestHandler<empleadolista, List<empleado>>
        {
             private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<List<empleado>> Handle(empleadolista request, CancellationToken cancellationToken)
            {
                var empleados = await _context.empleado.ToListAsync();
                return empleados;
            }
        }
    }
}