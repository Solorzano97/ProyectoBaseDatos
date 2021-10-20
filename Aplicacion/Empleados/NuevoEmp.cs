using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Empleados
{
    public class NuevoEmp
    {
        public class nuevoemp : IRequest {
            
             [Required(ErrorMessage ="el campo nombre es requerido")]   
            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")] 
            public string Nombre {get;set;}

             [Required(ErrorMessage ="el campo nombre es requerido")]   
            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")] 
            public string Apellido {get;set;}

            [Required(ErrorMessage ="el campo correo es  requerido")]
            [EmailAddress(ErrorMessage = "No tiene el formato de email")]
            public string Correo {get; set;}

            [Required(ErrorMessage ="el campo contraseña  es  requerido")]        
            public string Contrasena {get ; set; }
            public string Telefono {get;set;}
            public string Direccion {get;set;}
        
            public int BodegaId{get; set;}
            public int RolId {get;set;}

        }

        public class Manejador : IRequestHandler<nuevoemp>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(nuevoemp request, CancellationToken cancellationToken)
            {
                var empleados = new empleado {
                    Nombre = request.Nombre ,
                    Apellido = request.Apellido ,
                    Correo = request.Correo ,
                    Contrasena = request.Contrasena , 
                    Telefono = request.Telefono , 
                    Direccion = request.Direccion , 
                    BodegaId = request.BodegaId , 
                    RolId = request.RolId 



                };

                _context.empleado.Add(empleados);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el empleado");
            }
        }
    }
}