using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Temporadas
{
    public class ConsultaIdTemporada
    {
         public class temporadaUnico : IRequest<temporada>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<temporadaUnico, temporada>
        {
             private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<temporada> Handle(temporadaUnico request, CancellationToken cancellationToken)
            {
                var t = await _context.temporada.FindAsync(request.Id);
                  if(t == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro la temporada"} );
                 }
                return t;
        }
    }
}
}