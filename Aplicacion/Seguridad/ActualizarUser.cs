using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;


namespace Aplicacion.Seguridad
{
    public class ActualizarUser
    {

        public class EditarUser : IRequest<UsuarioData>{

            
            
            [Required(ErrorMessage ="el campo nombre es requerido")]   
            [RegularExpression("[a-zA-Z]{2,20}", 
            ErrorMessage = "Solo admite    letras entre 2 y 20")] 
            public string Nombre{get;set;}
            [Required(ErrorMessage ="el campo correo es  requerido")]
            public string Apellidos{get;set;}

            [Required(ErrorMessage ="el campo correo es  requerido")]
            [EmailAddress(ErrorMessage = "No tiene el formato de email")]
            public string Email {get;set;}
            [Required(ErrorMessage ="el campo correo es  requerido")]
            public string Password{get;set;}

            [Required(ErrorMessage ="el campo correo es  requerido")]
            public string Username{get; set;}

        }

        public class Manejador : IRequestHandler<EditarUser, UsuarioData>
        {
             private readonly InventarioOnlineContext _context;
            private readonly UserManager<usuario> _userManager ;
            private readonly IJwtGenerador _jwtGenerador;

            private readonly IPasswordHasher<usuario> _passwordhasher;
             public Manejador(InventarioOnlineContext context , UserManager<usuario> userManager, IJwtGenerador jwtGenerador , IPasswordHasher<usuario> passwordhasher){

                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _passwordhasher = passwordhasher;
            }
            public async Task<UsuarioData> Handle(EditarUser request, CancellationToken cancellationToken)
            {
                 var u = await _userManager.FindByNameAsync(request.Username);
                 if(u == null){
                     throw new ManejadorExcepcion(HttpStatusCode.NotFound, new{bodega = "no se encontro el username"} );
                 }

                 var existeUsername = await _context.Users.Where( x=> x.Email == request.Email && x.UserName != request.Username).AnyAsync();

                 if(existeUsername){
                     throw new ManejadorExcepcion(HttpStatusCode.InternalServerError , new {mensaje = "este email ya pertenece a otro usuario"});
                 }


                 
                 u.nombreCompleto = request.Nombre +" " + request.Apellidos;
                 u.Email = request.Email;
                 u.PasswordHash = _passwordhasher.HashPassword(u , request.Password);
                 

                 var respuestaUpdate = await _userManager.UpdateAsync(u);

                  

                if(respuestaUpdate.Succeeded){
                    return new UsuarioData{
                        nombreCompleto = u.nombreCompleto ,
                        Tokem = _jwtGenerador.CrearToken(u),
                        Username = u.UserName ,
                        Email = u.Email
                    };
                }

                throw new Exception("no se pudo actualizar el ususario");

            }
        }
    }
}