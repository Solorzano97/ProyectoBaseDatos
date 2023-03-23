using System.Collections.Generic;


namespace Dominio
{
    public class bodega
    {
     
        public int BodegaId {get;set;}
        public string nombre {get; set;}
        public string direccion {get;set;}
        public string telefono {get; set;}
        public string email {get;set;}
        
        

        public ICollection<existencias> existenciasbodega {get;set;} //relacion
        public ICollection<empleado> empleados {get;set;} //relacion
         public ICollection<movimientos> movimientosbodega {get;set;} //relacion



    }
}