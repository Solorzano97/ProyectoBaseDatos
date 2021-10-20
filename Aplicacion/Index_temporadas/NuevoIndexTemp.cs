using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Index_temporadas
{
    public class NuevoIndexTemp
    {
        public class newindext : IRequest{

            public int TemporadaId {get;set;}
            public int ProductoId {get;set;}

        }

        public class Manejador : IRequestHandler<newindext>
        {
             private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newindext request, CancellationToken cancellationToken)
            {
                var indext = new indexTemporada(){

                    TemporadaId = request.TemporadaId ,
                    ProductoId = request.ProductoId

                };

                 _context.indexTemporada.Add(indext);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el index de la temporada");
            }
        }

    }
}