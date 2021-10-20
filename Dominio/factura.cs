using System;
using System.Collections.Generic;


namespace Dominio
{
    public class factura
    {
       
        public int FacturaId {get;set;}
       
        public double total {get;set;}
        public string nit {get;set;}
        public string nombre {get;set;}
        public DateTime FechaHora {get;set;}

        public int MetodoPagoId {get;set;}

        public int ClienteId {get;set;}
        public int EmpleadoId {get;set;}

        public empleado empleado{get;set;}//ancla

        public cliente cliente{get;set;}//ancla

        public metodoPago metodoPago{get;set;}//ancal

         public ICollection<detalle> DetalleFACT {get;set;} //relacion
    }
}