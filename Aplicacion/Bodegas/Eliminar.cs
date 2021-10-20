using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Bodegas
{
    public class Eliminar
    {
        public class EliminarBodega : IRequest{

            public int Id {get;set;}

        }

        public class Manejador : IRequestHandler<EliminarBodega>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<Unit> Handle(EliminarBodega request, CancellationToken cancellationToken)
            {
                var bodegas =  await _context.bodega.FindAsync(request.Id);
                if(bodegas == null){
                    //throw new Exception("no se puede elimnar el curso");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se econtro la bodega"} );
                }

                _context.Remove(bodegas);

                var resultado = await _context.SaveChangesAsync();

                if(resultado>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo eliminar");
            }
        }
    }
}