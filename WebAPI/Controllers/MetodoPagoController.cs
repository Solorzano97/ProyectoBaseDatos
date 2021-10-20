using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.MetodoPagos;
using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public MetodoPagoController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoMetodoP.newmetodopago data){ 
            return await _mediator.Send(data);
        }


        [HttpGet]  //listar todo
        public async Task<ActionResult<List<metodoPago>>> Get(){

            return await _mediator.Send(new ConsultaMetodoP.metodopagolista());


        }

         [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<metodoPago>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdMetodoP.metodoUnico{Id = id});
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarMetodoP.Editarmetodopago data){

            data.MetodoPagoId = id;
            return await _mediator.Send(data);
        }

        
    }
}