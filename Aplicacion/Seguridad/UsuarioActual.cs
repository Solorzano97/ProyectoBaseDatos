using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecutar : IRequest<UsuarioData> {}

        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly UserManager<usuario> _userManager;
            private readonly IJwtGenerador _jwtGenrador;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manejador(UserManager<usuario> userManager , IJwtGenerador jwtGenerador , IUsuarioSesion usuarioSesion){
                    _userManager = userManager;
                    _jwtGenrador = jwtGenerador;
                    _usuarioSesion = usuarioSesion;
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var user =  await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
                return new UsuarioData{
                    nombreCompleto = user.nombreCompleto,
                    Username = user.UserName ,
                    Tokem = _jwtGenrador.CrearToken(user),
                    Imagen = null,
                    Email = user.Email
                };
                
            }
        }
    }
}