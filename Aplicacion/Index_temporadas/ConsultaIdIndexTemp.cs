using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Index_temporadas
{
    public class ConsultaIdIndexTemp
    {

        public class indextempUnico : IRequest<indexTemporada>{
            public int Id{get;set;}
        }

        public class Manejador : IRequestHandler<indextempUnico, indexTemporada>
        {
             private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<indexTemporada> Handle(indextempUnico request, CancellationToken cancellationToken)
            {
                var indextemp = await _context.indexTemporada.FindAsync(request.Id);
                if(indextemp == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro index de temporada"} );
                 }
                return indextemp;
            }
        }

    }
}