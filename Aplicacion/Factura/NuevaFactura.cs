using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Factura
{
    public class NuevaFactura
    {
        public class newfact : IRequest{



            [Required(ErrorMessage ="el campo total es requerido")]
            [Range(0, 100000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 100000")]
            public double total {get;set;}

            [Required(ErrorMessage ="el campo nit es requerido")]
            public string nit {get;set;}

            [Required(ErrorMessage ="el campo nombre es requerido")]   
            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")]
            public string nombre {get;set;}
            public DateTime FechaHora {get;set;}

            public int MetodoPagoId {get;set;}

            public int ClienteId {get;set;}
            public int EmpleadoId {get;set;}

        }

        public class Manejador : IRequestHandler<newfact>
        {
            private readonly InventarioOnlineContext _context;
            public Manejador(InventarioOnlineContext context){

                _context = context;
                
            }
            public async Task<Unit> Handle(newfact request, CancellationToken cancellationToken)
            {
                var facts = new factura{
                    total = request.total ,
                    nit = request.nit ,
                    nombre = request.nombre ,
                    FechaHora = request.FechaHora ,
                    MetodoPagoId = request.MetodoPagoId ,
                    ClienteId = request.ClienteId ,
                    EmpleadoId = request.EmpleadoId


                };

                 _context.factura.Add(facts);
                var valor = await _context.SaveChangesAsync();

                if(valor>0){
                    return Unit.Value;
                }

                throw new Exception("no se pudo insertar la factura");
            }
        }
    }
}