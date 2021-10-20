using System.Collections.Generic;
namespace Dominio

{
    public class detalle
    {
        
        public int DetalleId {get; set;}
        public int cantidad {get;set;}
        public double subtotal {get;set;}

        public int ProductoId {get;set;}
        public int FacturaId {get;set;}

        public factura factura {get;set;}//ancla

        public producto producto {get;set;}//ancla
       
    }
}