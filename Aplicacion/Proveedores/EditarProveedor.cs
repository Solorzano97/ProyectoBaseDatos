using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Proveedores
{
    public class EditarProveedor
    {
        public class Editarprov : IRequest{
            public int ProveedorId {get;set;}

            [RegularExpression("[a-zA-Z]{2,20}", 
        ErrorMessage = "Solo admite    letras entre 2 y 20")]
        public string Nombre {get;set;}
        public string Direccion {get;set;}
        public string Telefono {get;set;}
        public DateTime FechaInicio {get;set;}

        [EmailAddress(ErrorMessage = "No tiene el formato de email")]
        public string Email {get;set;}
        }

        public class Manejador : IRequestHandler<Editarprov>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarprov request, CancellationToken cancellationToken)
            {
               var pro = await _context.proveedor.FindAsync(request.ProveedorId);
                  if(pro == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro el proveedor"} );
                 }

                 pro.Nombre = request.Nombre ?? pro.Nombre;
                 pro.Direccion = request.Direccion ?? pro.Direccion ;
                 pro.Telefono = request.Telefono ?? pro.Telefono ;
                 pro.FechaInicio = request.FechaInicio;
                 pro.Email = request.Email ?? pro.Email;

                  var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}