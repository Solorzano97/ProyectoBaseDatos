using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Categorias
{
    public class EliminarCat
    {

        public class Eliminarcategoria : IRequest{

            public int Id {get;set;}
        }

        public class Manejador : IRequestHandler<Eliminarcategoria>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;

            }
            public async Task<Unit> Handle(Eliminarcategoria request, CancellationToken cancellationToken)
            {
                 var categorias =  await _context.categoria.FindAsync(request.Id);
               if(categorias == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se econtro la categoria"} );
                 }

                _context.Remove(categorias);

                var resultado = await _context.SaveChangesAsync();

                if(resultado>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo eliminar");
                
            }
        }
    }
}