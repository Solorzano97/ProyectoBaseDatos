using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Categorias
{
    public class EditarCat
    {

        public class Editarcategoria : IRequest{
             public int CategoriaId {get; set;}
             public string NombreCategoria {get; set;}
        }


        public class Manejador : IRequestHandler<Editarcategoria>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarcategoria request, CancellationToken cancellationToken)
            {
                 var categorias = await _context.categoria.FindAsync(request.CategoriaId);
                if(categorias == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se econtro la categoria"} );
                 }

                 categorias.NombreCategoria = request.NombreCategoria ?? categorias.NombreCategoria;
              

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }

    }
}