using System.Collections.Generic;


namespace Dominio
{
    public class empleado
    {
      
        public int EmpleadoId {get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}

        public string Correo {get; set;}

        public string Contrasena {get ; set; }
        public string Telefono {get;set;}
        public string Direccion {get;set;}
        
        public int BodegaId{get; set;}
        public int RolId {get;set;}

        public bodega bodega {get;set;}//ancla
        public rol rol{get;set;}//ancla

         public ICollection<factura> Factura {get;set;} //relacion
    }
}