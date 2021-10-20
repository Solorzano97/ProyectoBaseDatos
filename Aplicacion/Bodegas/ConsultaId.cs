using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Bodegas
{
    public class ConsultaId
    {
        public class bodegaUnica : IRequest<bodega>{

            public int Id {get;set;}


        }

            
    

    public class Manejador : IRequestHandler<bodegaUnica , bodega>{

         private readonly InventarioOnlineContext _context;
        public Manejador(InventarioOnlineContext context){
            _context = context;
        }

        public async Task<bodega> Handle(bodegaUnica request, CancellationToken cancellationToken)
        {
            var bodegas = await _context.bodega.FindAsync(request.Id);

            if(bodegas == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se econtro la bodega"} );
                 }


            return bodegas;
        }
    }

    }
    
       

        
    
}