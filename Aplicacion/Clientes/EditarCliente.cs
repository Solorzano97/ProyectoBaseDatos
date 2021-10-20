using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Clientes
{
    public class EditarCliente
    {
        public class Editarcli : IRequest{
        public int ClienteId {get;set;}

         
        [RegularExpression("[a-zA-Z]{2,20}", 
        ErrorMessage = "Solo admite    letras entre 2 y 20")] 
        public string nombre {get;set;}

        [RegularExpression("[a-zA-Z]{2,20}", 
        ErrorMessage = "Solo admite    letras entre 2 y 20")] 
        public string apellido {get;set;}
        public string nombreUsuario {get;set;}
        public string nit {get;set;}

        [EmailAddress(ErrorMessage = "No tiene el formato de email")]
        public string correo {get;set;}
        public string contrasena {get;set;}
        public string tarjeta {get;set;}
        public string nombreTarjeta {get;set;}
        }

        public class Manejador : IRequestHandler<Editarcli>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarcli request, CancellationToken cancellationToken)
            {
                  var cli = await _context.cliente.FindAsync(request.ClienteId);
                 if(cli == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontro el cliente"} );
                 }

                 cli.nombre = request.nombre ?? cli.nombre;
                 cli.apellido = request.apellido ?? cli.apellido;
                 cli.nombreUsuario = request.nombreUsuario ?? cli.nombreUsuario;
                 cli.nit = request.nit ?? cli.nit;
                 cli.correo = request.correo ?? cli.correo ;
                 cli.contrasena = request.contrasena ?? cli.contrasena ;
                 cli.tarjeta = request.tarjeta ?? cli.tarjeta ;
                 cli.nombreTarjeta = request.nombreTarjeta ?? cli.nombreTarjeta;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}