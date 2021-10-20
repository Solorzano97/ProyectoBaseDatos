using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Producto;
using Aplicacion.TipoMovimientos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class TipoMovimientoController : ControllerBase
    {
         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public TipoMovimientoController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoTipoMov.newtipomovi data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<tipoMovimiento>>> Get(){

            return await _mediator.Send(new ConsultaTipoMov.tipomovlista());


        }

        [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<tipoMovimiento>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdTipoMov.Unico{Id = id});
        }

          [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarTipoMov.Editartipomovimiento data){

            data.TipoMovimientoId = id;
            return await _mediator.Send(data);
        }
    }
}