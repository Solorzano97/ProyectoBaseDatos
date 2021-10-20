using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Empleados
{
    public class EditarEmpleado
    {
        public class Editaremp : IRequest{
            public int EmpleadoId {get;set;}

            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")] 
            public string Nombre {get;set;}

            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")] 
            public string Apellido {get;set;}
            
            [EmailAddress(ErrorMessage = "No tiene el formato de email")]
            public string Correo {get; set;}

            public string Contrasena {get ; set; }
            public string Telefono {get;set;}
            public string Direccion {get;set;}
        
            public int BodegaId{get; set;}
            public int RolId {get;set;}
        }

        public class Manejador : IRequestHandler<Editaremp>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editaremp request, CancellationToken cancellationToken)
            {
                 var emple = await _context.empleado.FindAsync(request.EmpleadoId);
                  if(emple == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontro el empleado"} );
                 }

                 
                 emple.Nombre = request.Nombre ?? emple.Nombre;
                 emple.Apellido = request.Apellido ?? emple.Apellido;
                 emple.Correo = request.Correo ?? emple.Correo;
                 emple.Contrasena = request.Contrasena ?? emple.Contrasena;
                 emple.Telefono = request.Telefono ?? emple.Telefono ;
                 emple.Direccion = request.Direccion ?? emple.Direccion ;
                 emple.BodegaId = request.BodegaId;
                 emple.RolId = request.RolId;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}