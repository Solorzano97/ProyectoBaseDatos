using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;
namespace Aplicacion.Proveedores
{
    public class NuevoProveedor
    {
        public class newproveedor : IRequest{


        [Required(ErrorMessage ="el campo nombre es requerido")]   
        [RegularExpression("[a-zA-Z]{2,20}", 
        ErrorMessage = "Solo admite    letras entre 2 y 20")]
        public string Nombre {get;set;}

         [Required(ErrorMessage ="el campo direccion es requerido")]
        public string Direccion {get;set;}

         [Required(ErrorMessage ="el campo telefono es requerido")]
        public string Telefono {get;set;}
        public DateTime FechaInicio {get;set;}

         [Required(ErrorMessage ="el campo correo es requerido")]
         [EmailAddress(ErrorMessage = "No tiene el formato de email")]
        public string Email {get;set;}
        


    }
        public class Manejador : IRequestHandler<newproveedor>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newproveedor request, CancellationToken cancellationToken)
            {
                var p = new proveedor{

                    Nombre = request.Nombre ,
                    Direccion = request.Direccion ,
                    Telefono = request.Telefono ,
                    FechaInicio = request.FechaInicio ,
                    Email = request.Email


            };

             _context.proveedor.Add(p);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el producto");

        }
    }
}
}