using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Index_temporadas;
using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class IndexTemporadaController : ControllerBase
    {

         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public IndexTemporadaController(IMediator mediator){

            _mediator = mediator;

        }

         [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoIndexTemp.newindext data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<indexTemporada>>> Get(){

            return await _mediator.Send(new ConsultaIndex.listaindextemp());


        }


        [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<indexTemporada>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdIndexTemp.indextempUnico{Id = id});
        }

         [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarIndexTemp.Editarindextemporada data){

            data.IndexTemporadaId = id;
            return await _mediator.Send(data);
        }
        

    }
}