using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Producto;
using Aplicacion.Seguridad;
using Aplicacion.TipoMovimientos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
     [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class UsuarioController : ControllerBase
    {
         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public UsuarioController(IMediator mediator){

            _mediator = mediator;

        }

         [HttpPost("login")] 

        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros){ 
            return await _mediator.Send(parametros);
        }
        
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecut parametros){

            return await _mediator.Send(parametros);

        }

        [HttpGet]
        public async Task<ActionResult<UsuarioData>> DevolverUsuario(){
            return await _mediator.Send(new UsuarioActual.Ejecutar());
        }

        [HttpPut] //updtate
        public async Task<ActionResult<UsuarioData>> ActualizarUsuario(ActualizarUser.EditarUser parametros){
            return await _mediator.Send(parametros);
        }

        
    }
}