using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;


namespace Aplicacion.Categorias
{
    public class ConsultaIdCat
    {

        public class categoriaUnica : IRequest<categoria>{

            public int Id {get;set;}


        }

        public class Manejador : IRequestHandler<categoriaUnica, categoria>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){
            _context = context;
        }
            public async Task<categoria> Handle(categoriaUnica request, CancellationToken cancellationToken)
            {
                var categorias = await _context.categoria.FindAsync(request.Id);
                if(categorias == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se econtro la categoria"} );
                 }
                return categorias;
            }
        }
    }
}