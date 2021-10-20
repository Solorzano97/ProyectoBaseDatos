using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Empleados
{
    public class ConsultaIdEmp
    {
         public class emepleadoUnico : IRequest<empleado>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<emepleadoUnico, empleado>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<empleado> Handle(emepleadoUnico request, CancellationToken cancellationToken)
            {
                var e = await _context.empleado.FindAsync(request.Id);
                 if(e == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontro el empleado"} );
                 }
                return e;
            }
        }
    }
}