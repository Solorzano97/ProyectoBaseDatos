using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Categorias
{
    public class NuevaCat
    {
        public class CategoriaNueva : IRequest{

            [Required(ErrorMessage ="el campo nombre de categoria es requerido")]
            public string NombreCategoria {get; set;}

        }

        public class Manejador : IRequestHandler<CategoriaNueva>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){

                _context = context;
            }
            public async Task<Unit> Handle(CategoriaNueva request, CancellationToken cancellationToken)
            {
                 var categorias = new categoria {
                    NombreCategoria = request.NombreCategoria //aca el requeste jala los valores de la clase que cramos primero , la de BodegaNueva 
                    
                };

                _context.categoria.Add(categorias); //usar add para agregar data en esta caso de la variable bodega de arriba
                var valor = await _context.SaveChangesAsync(); //devolvera un valor ya sea 0 o 1 si es uno o mas si fue exitoso si no no
                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar la categoria");
            }
        }
    }
}