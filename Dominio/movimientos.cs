
namespace Dominio
{
    public class movimientos
    {
        
        public int MovimientosId {get;set;}
        public int cantidad {get;set;}
        public int BodegaId {get;set;}
        public int ProductoId {get;set;}
        public int TipoMovimientoId {get;set;}
        public int BodegaId2 {get;set;} 

        

        public producto producto {get;set;} //ancal
        public tipoMovimiento tipoMovimiento{get;set;} //ancla

        public bodega bodega {get;set;} //ancla
    }
}