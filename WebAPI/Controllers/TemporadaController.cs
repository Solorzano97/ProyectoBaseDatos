using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Producto;
using Aplicacion.Temporadas;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va

    public class TemporadaController : ControllerBase

    {
        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public TemporadaController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevaTemporada.newtemporada data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<producto>>> Get(){

            return await _mediator.Send(new ConsultaPro.productolista());


        }

          [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<temporada>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdTemporada.temporadaUnico{Id = id});
        }


        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarTemporada.Editartemp data){

            data.TemporadaId = id;
            return await _mediator.Send(data);
        }

    }
}