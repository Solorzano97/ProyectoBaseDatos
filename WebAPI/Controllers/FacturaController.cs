using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Factura;
using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va

    public class FacturaController
    {

         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public FacturaController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear
        public async Task<ActionResult<Unit>> Crear(NuevaFactura.newfact data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<factura>>> Get(){

            return await _mediator.Send(new ConsultaFact.listafacturas());


        }


         [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<factura>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdFact.facturaUnica{Id = id});
        }

         [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarFact.Editarfactura data){

            data.FacturaId = id;
            return await _mediator.Send(data);
        }
        
    }
}