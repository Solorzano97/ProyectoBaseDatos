using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Empleados;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers

{
    [Route("api/[controller]")] 
    [ApiController]
    public class EmpleadoController : ControllerBase
    
    {
         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
         public EmpleadoController(IMediator mediator){

            _mediator = mediator;

        }

        
        [HttpGet]  //listar todo
        public async Task<ActionResult<List<empleado>>> Get(){

            return await _mediator.Send(new ConsultaEmp.empleadolista());


        }

        [HttpPost] //nuevo empleado
        public async Task<ActionResult<Unit>> Crear(NuevoEmp.nuevoemp data){
            return await _mediator.Send(data);
        }

         [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarEmpleado.Editaremp data){

            data.EmpleadoId = id;
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<empleado>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdEmp.emepleadoUnico{Id = id});
        }
    }
}