using System.Collections.Generic;


namespace Dominio
{
    public class tipoMovimiento
    {
        
        public int TipoMovimientoId {get;set;}
        public string descripcion {get;set;}
        
         public ICollection<movimientos> movimientos_tipo {get;set;} //relacion
    }
}