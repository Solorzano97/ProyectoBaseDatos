using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Temporadas
{
    public class NuevaTemporada
    {
        public class newtemporada : IRequest{

            [Required(ErrorMessage ="el campo nombre es requerido")]
            public string nombre {get;set;}

        }
        public class Manejador : IRequestHandler<newtemporada>
        {
             private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newtemporada request, CancellationToken cancellationToken)
            {
                 var temp = new temporada{

                     nombre = request.nombre

                 };

                 _context.temporada.Add(temp);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar la temporada");
            }
        }

    }
}