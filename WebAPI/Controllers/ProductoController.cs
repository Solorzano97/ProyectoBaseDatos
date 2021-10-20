using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Producto;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class ProductoController : ControllerBase

    {
        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public ProductoController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoProduct.newproducto data){ 
            return await _mediator.Send(data);
        }

        [HttpGet]  //listar todo
        public async Task<ActionResult<List<producto>>> Get(){

            return await _mediator.Send(new ConsultaPro.productolista());


        }

          [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<producto>> Detalle(int id){
            return await _mediator.Send(new ConsultaId.productUnico{Id = id});
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarPro.Editarproduct data){

            data.ProductoId = id;
            return await _mediator.Send(data);
        }
       
    }
}