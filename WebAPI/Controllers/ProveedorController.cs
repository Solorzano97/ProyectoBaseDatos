using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Producto;
using Aplicacion.Proveedores;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] //constantes siempre van
    [ApiController]//siempre va
    public class ProveedorController
    {
        private readonly IMediator _mediator; //confihuracion pricipal y agregar en startup como mediatr
        public ProveedorController(IMediator mediator){

            _mediator = mediator;

        }

        [HttpPost] // crear

        public async Task<ActionResult<Unit>> Crear(NuevoProveedor.newproveedor data){ 
            return await _mediator.Send(data);
        }


         [HttpGet]  //listar todo
        public async Task<ActionResult<List<proveedor>>> Get(){

            return await _mediator.Send(new ConsultaProveedor.proveedorlista());


        }

          [HttpGet("{id}")] //listar por id
        public async Task<ActionResult<proveedor>> Detalle(int id){
            return await _mediator.Send(new ConsultaIdProveedor.proveedorUnico{Id = id});
        }

        [HttpPut("{id}")] //editar
        public async Task<ActionResult<Unit>> Editar(int id , EditarProveedor.Editarprov data){

            data.ProveedorId = id;
            return await _mediator.Send(data);
        }
        
    }
}