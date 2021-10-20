using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Clientes;
using Aplicacion.Empleados;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
         public ClienteController(IMediator mediator){

            _mediator = mediator;

        }

         [HttpPost] //nuevo empleado
        public async Task<ActionResult<Unit>> Crear(NuevoCliente.newcliente data){
            return await _mediator.Send(data);
        }

         [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<cliente>> Detalle(int id){
            return await _mediator.Send(new CosultaIdCli.clienteUnico {Id = id});
        }

         [HttpGet]  //listar todo
        public async Task<ActionResult<List<cliente>>> Get(){

            return await _mediator.Send(new ConsultaCliente.clienteslista());


        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarCliente.Editarcli data){

            data.ClienteId = id;
            return await _mediator.Send(data);
        }
    }
}