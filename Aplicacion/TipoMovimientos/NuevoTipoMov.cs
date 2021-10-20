using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;
namespace Aplicacion.TipoMovimientos
{
    public class NuevoTipoMov
    {
        public class newtipomovi : IRequest{
            public string descripcion {get;set;}
        }
        public class Manejador : IRequestHandler<newtipomovi>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newtipomovi request, CancellationToken cancellationToken)
            {
                 var tipo = new tipoMovimiento{

                     descripcion = request.descripcion

                 };

                 _context.tipoMovimiento.Add(tipo);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el tipo de movimienot");
            }
        }
    }
}