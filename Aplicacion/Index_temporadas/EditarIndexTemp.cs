using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Index_temporadas
{
    public class EditarIndexTemp
    {
        public class Editarindextemporada : IRequest{

            public int IndexTemporadaId {get;set;}
            public int TemporadaId {get;set;}
            public int ProductoId {get;set;}

        }

        public class Manejador : IRequestHandler<Editarindextemporada>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarindextemporada request, CancellationToken cancellationToken)
            {
                var indextemp = await _context.indexTemporada.FindAsync(request.IndexTemporadaId);
                  if(indextemp == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro index de temporada"} );
                 }


                 
                 indextemp.TemporadaId = request.TemporadaId;
                 indextemp.ProductoId = request.ProductoId;

                  var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}