using System.Collections.Generic;


namespace Dominio
{
    public class metodoPago
    {
        
        public int MetodoPagoId {get;set;}
        public string nombre {get;set;}
        
         public ICollection<factura> factura_metodopago {get;set;} //relacion
    }
}