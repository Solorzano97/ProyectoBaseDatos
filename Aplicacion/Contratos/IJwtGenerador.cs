using Dominio;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerador
    {
         string CrearToken(usuario user);
    }
}