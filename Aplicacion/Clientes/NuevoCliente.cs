using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Clientes
{
    public class NuevoCliente
    {
         public class newcliente : IRequest{

        [Required(ErrorMessage ="el campo nombre es requerido")]   
        [RegularExpression("[a-zA-Z]{2,20}", 
        ErrorMessage = "Solo admite    letras entre 2 y 20")] 
        public string nombre {get;set;}

        [Required(ErrorMessage ="el campo Apellido es requerido")]   
        [RegularExpression("[a-zA-Z]{2,20}", 
        ErrorMessage = "Solo admite    letras entre 2 y 20")] 
        public string apellido {get;set;}

        [Required(ErrorMessage ="el campo nombre de usuario es  requerido")]
        public string nombreUsuario {get;set;}

        [Required(ErrorMessage ="el campo nit  es  requerido")]
        public string nit {get;set;}

        [Required(ErrorMessage ="el campo correo es  requerido")]
        [EmailAddress(ErrorMessage = "No tiene el formato de email")]
        public string correo {get;set;}

        [Required(ErrorMessage ="el campo contraseña es  requerido")]
        public string contrasena {get;set;}
        public string tarjeta {get;set;}
        public string nombreTarjeta {get;set;}
         }

       

        public class Manejador : IRequestHandler<newcliente>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async  Task<Unit> Handle(newcliente request, CancellationToken cancellationToken)
            {
               var cli = new cliente{

                   nombre = request.nombre ,
                   apellido = request.apellido ,
                   nombreUsuario = request.nombreUsuario ,
                   nit = request.nit ,
                   correo = request.correo ,
                   contrasena = request.contrasena ,
                   tarjeta = request.tarjeta ,
                   nombreTarjeta = request.nombreTarjeta

               };

               _context.cliente.Add(cli);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar el cliente");
            }
        }
    }
}