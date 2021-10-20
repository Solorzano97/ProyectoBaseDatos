using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Movimiento;
using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class MovimientosController
    {
        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public MovimientosController(IMediator mediator){

            _mediator = mediator;

        }

         [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoMov.newmovimiento data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<movimientos>>> Get(){

            return await _mediator.Send(new ConsultaMov.movlista());


        }


         [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<movimientos>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdMov.movUnico{Id = id});
        }

         [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarMov.Editarmovimiento data){

            data.MovimientosId = id;
            return await _mediator.Send(data);
        }
        
    }
}