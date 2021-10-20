using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Temporadas
{
    public class EditarTemporada
    {
        public class Editartemp : IRequest{

            public int TemporadaId {get;set;}
        public string nombre {get;set;}

        }
        public class Manejador : IRequestHandler<Editartemp>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editartemp request, CancellationToken cancellationToken)
            {
                var t = await _context.temporada.FindAsync(request.TemporadaId);
                  if(t == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro la temporada"} );
                 }

                 t.nombre = request.nombre ?? t.nombre;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}