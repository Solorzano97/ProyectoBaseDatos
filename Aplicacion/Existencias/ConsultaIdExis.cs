using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Existencias
{
    public class ConsultaIdExis
    {
        public class existenciaUnica : IRequest<existencias>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<existenciaUnica, existencias>
        {

               private readonly InventarioOnlineContext _context;
                public Manejador(InventarioOnlineContext context){
                _context = context;
                 }
            public async Task<existencias> Handle(existenciaUnica request, CancellationToken cancellationToken)
            {
                var exist = await _context.existencias.FindAsync(request.Id);
                if(exist == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontraron existencias"} );
                 }
                return exist;
            }
        }
    }
}