using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Detalle;
using Aplicacion.Detalles;
using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class DetalleController : ControllerBase
    {

         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public DetalleController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoDeta.newdetalle data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<detalle>>> Get(){

            return await _mediator.Send(new ConsultaDeta.listadetalles());


        }

          [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<detalle>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdDeta.detalleUnico{Id = id});
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarDeta.Editardetalle data){

            data.DetalleId = id;
            return await _mediator.Send(data);
        }
        
    }
}