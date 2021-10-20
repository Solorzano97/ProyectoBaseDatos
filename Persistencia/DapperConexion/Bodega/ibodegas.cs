using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Bodega
{
    public interface ibodegas
    {
         Task<IEnumerable<BodegaModelo>> ObtenerLista();

         Task<BodegaModelo> ObtenerPorId (int id);

         Task<int> Actualiza(BodegaModelo parametros);

         Task<int> Nuevo (BodegaModelo parametros);
    }
}