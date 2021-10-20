using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Bodega;
using Aplicacion.Bodegas;
using Aplicacion.Categorias;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class CategoriaController : ControllerBase
    {

         private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public CategoriaController(IMediator mediator){

            _mediator = mediator;

        }


        [HttpGet]  //listar todo
        public async Task<ActionResult<List<categoria>>> Get(){

            return await _mediator.Send(new ConsultaCat.categorialista());


        }


        //http://localhost:5000/api/Bodega/1 
        [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<categoria>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdCat.categoriaUnica{Id = id});
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarCat.Editarcategoria data){

            data.CategoriaId = id;
            return await _mediator.Send(data);
        }


        [HttpDelete("{id}")] //Eliminar
        public async Task<ActionResult<Unit>> Eliminar(int id){
            return await _mediator.Send(new EliminarCat.Eliminarcategoria{Id = id});
        }

        
        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevaCat.CategoriaNueva data){ //nuevo.bodeganueva el la clase que esta en nuevo
            return await _mediator.Send(data);
        }
    
    }
}