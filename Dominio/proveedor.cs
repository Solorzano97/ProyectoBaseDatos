using System;
using System.Collections.Generic;


namespace Dominio
{
    public class proveedor
    {
        
        public int ProveedorId {get;set;}
        public string Nombre {get;set;}
        public string Direccion {get;set;}
        public string Telefono {get;set;}
        public DateTime FechaInicio {get;set;}
        public string Email {get;set;}
        
         public ICollection<existencias> existenciasproveedor {get;set;} //relacion


    }
}