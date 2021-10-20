
namespace Dominio
{
    public class existencias
    {
       
        public int ExistenciasId {get;set;}
        public int cantidad {get;set;}
        public int ProductoId {get;set;}
        public int ProveedorId {get;set;}
        public int BodegaId {get;set;}

        public producto producto{get;set;} //ancla

        public proveedor proveedor {get;set;}//ancla

        public bodega bodega {get;set;}//ancla
    }
}