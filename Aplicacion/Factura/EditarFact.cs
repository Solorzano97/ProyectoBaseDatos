using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Factura
{
    public class EditarFact
    {
        public class Editarfactura : IRequest{
            public int FacturaId {get;set;}

            [Range(0, 100000, 
            ErrorMessage = "La cantidad debe estar comprendida entre 0 y 100000")]
            public double total {get;set;}
            public string nit {get;set;}
            public string nombre {get;set;}
            public DateTime FechaHora {get;set;}

            public int MetodoPagoId {get;set;}

            public int ClienteId {get;set;}
            public int EmpleadoId {get;set;}
        }

        public class Manejador : IRequestHandler<Editarfactura>
        {
            private readonly InventarioOnlineContext _context;

            public Manejador(InventarioOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Editarfactura request, CancellationToken cancellationToken)
            {
                var fact = await _context.factura.FindAsync(request.FacturaId);
                 if(fact == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{mensaje = "no se encontro factura"} );
                 }

                 
                 fact.total = request.total;
                 fact.nit = request.nit ?? fact.nit;
                 fact.nombre = request.nombre ?? fact.nombre;
                 fact.FechaHora = request.FechaHora ;
                 fact.MetodoPagoId = request.MetodoPagoId ;
                 fact.ClienteId = request.ClienteId ;
                 fact.EmpleadoId = request.EmpleadoId;

                 var resultado = await _context.SaveChangesAsync();

                 if(resultado>0){
                     return Unit.Value;
                 }

                 throw new Exception("NO SE GUARDARON LOS CAMBIOS");
            }
        }
    }
}