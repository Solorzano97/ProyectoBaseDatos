using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Proveedores
{
    public class ConsultaIdProveedor
    {
         public class proveedorUnico : IRequest<proveedor>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<proveedorUnico, proveedor>
        {
             private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<proveedor> Handle(proveedorUnico request, CancellationToken cancellationToken)
            {
                var pro = await _context.proveedor.FindAsync(request.Id);
                 if(pro == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el proveedor"} );
                 }
                return pro;
            }
        }
    }
}