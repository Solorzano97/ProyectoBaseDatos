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
    public class Registrar
    {
        public class Ejecut : IRequest<UsuarioData>{

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

     

        public class Manejador : IRequestHandler<Ejecut , UsuarioData>
        {
            private readonly InventarioOnlineContext _context;
            private readonly UserManager<usuario> _userManager ;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(InventarioOnlineContext context , UserManager<usuario> userManager, IJwtGenerador jwtGenerador){

                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                
            }
            public async Task<UsuarioData> Handle(Ejecut request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where( x => x.Email == request.Email).AnyAsync();
                if(existe){
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest , new {mensaje = "el email ingresado ya existe"});
                }

                var existeUsername = await _context.Users.Where( x=> x.UserName == request.Username).AnyAsync();
                 if(existeUsername){
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest , new {mensaje = "el username ingresado ya existe"});
                }

                var user = new usuario{
                    nombreCompleto= request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.Username

                };

                var resultado = await _userManager.CreateAsync(user , request.Password);

                if(resultado.Succeeded){
                    return new UsuarioData{
                        nombreCompleto = user.nombreCompleto ,
                        Tokem = _jwtGenerador.CrearToken(user),
                        Username = user.UserName ,
                        Email = user.Email
                    };
                }

                throw new Exception("no se pudo agreagar el ususario");

            }
        }
    }
}