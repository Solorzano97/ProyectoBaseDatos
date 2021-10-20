using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Bodega;
using Aplicacion.Bodegas;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //http://localhost:5000/api/Bodega
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class BodegaController : ControllerBase
    {

        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public BodegaController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpGet]  //listar todo
        [Authorize] // para autorizar con token
        public async Task<ActionResult<List<bodega>>> Get(){

            return await _mediator.Send(new Consulta.bodegalista());


        }

        //http://localhost:5000/api/Bodega/1 
        [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<bodega>> Detalle(int id){
            return await _mediator.Send(new ConsultaId.bodegaUnica{Id = id});
        }


        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(Nuevo.BodegaNueva data){ //nuevo.bodeganueva el la clase que esta en nuevo
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , Editar.EditarBodega data){

            data.BodegaId = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id){
            return await _mediator.Send(new Eliminar.EliminarBodega{Id = id});
        }

        }
    }
