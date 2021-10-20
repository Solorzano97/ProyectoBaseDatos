using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Bodega;
using Aplicacion.Bodegas;
using Aplicacion.Categorias;
using Aplicacion.Roles;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class RolController : ControllerBase
    {

         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public RolController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<rol>>> Get(){

            return await _mediator.Send(new ConsultaRol.rollista());


        }

        //http://localhost:5000/api/Bodega/1 
        [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<rol>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdRol.rolUnico{Id = id});
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarRol.Editarrol data){

            data.RolId = id;
            return await _mediator.Send(data);
        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoRol.RolaNuevo data){ //nuevo.bodeganueva el la clase que esta en nuevo
            return await _mediator.Send(data);
        }

        
    }
}