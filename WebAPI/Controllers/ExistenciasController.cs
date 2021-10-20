using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Existencias;
using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class ExistenciasController
    {

        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public ExistenciasController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevaExis.newexist data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<existencias>>> Get(){

            return await _mediator.Send(new ConsultaExis.listaexistencias());


        }

         [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<existencias>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdExis.existenciaUnica{Id = id});
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarExis.Editarexistencia data){

            data.ExistenciasId = id;
            return await _mediator.Send(data);
        }
        
    }
}