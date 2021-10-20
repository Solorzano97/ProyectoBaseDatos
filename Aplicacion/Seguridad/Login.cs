using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>{

            [Required(ErrorMessage ="el campo correo es  requerido")]
            [EmailAddress(ErrorMessage = "No tiene el formato de email")]
            public string Email {get ; set;}

            [Required(ErrorMessage = "La password es obligatorio")]
            [DataType(DataType.Password)]
            public string Password {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<usuario> _userManager;

            private readonly SignInManager<usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(UserManager<usuario> userManager , SignInManager<usuario> signInManager , IJwtGenerador jwtGenerador){

                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;



            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user == null){
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(user, request.Password , false);
                if(resultado.Succeeded){
                    return new UsuarioData{
                        nombreCompleto = user.nombreCompleto,
                        Tokem = _jwtGenerador.CrearToken(user),
                        Username = user.UserName ,
                        Email = user.Email ,
                        Imagen = null
                    };
                }

                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                
            }
        }
    }
}